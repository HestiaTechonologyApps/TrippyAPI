using Microsoft.EntityFrameworkCore;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IServices;
using Trippy.InfraCore.Data;

namespace Trippy.Api.Jobs
{
  
        public class NotificationBackgroundWorker : BackgroundService
        {
            private readonly IServiceProvider _services;
            private readonly ILogger<NotificationBackgroundWorker> _logger;
            private readonly IConfiguration _config;

            private readonly int _batchSize;
            private readonly TimeSpan _pollInterval;
            private readonly int _maxRetries;
            private readonly TimeSpan _retryBase;

            public NotificationBackgroundWorker(IServiceProvider services, IConfiguration config, ILogger<NotificationBackgroundWorker> logger)
            {
                _services = services;
                _logger = logger;
                _config = config;

                _batchSize = _config.GetValue<int?>("Notifications:BatchSize") ?? 50;
                _pollInterval = TimeSpan.FromSeconds(_config.GetValue<int?>("Notifications:PollIntervalSeconds") ?? 10);
                _maxRetries = _config.GetValue<int?>("Notifications:MaxRetries") ?? 5;
                _retryBase = TimeSpan.FromSeconds(_config.GetValue<int?>("Notifications:RetryBaseSeconds") ?? 30);
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                _logger.LogInformation("Notification worker started.");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await ProcessBatchAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Worker encountered an error");
                    }

                    await Task.Delay(_pollInterval, stoppingToken);
                }

                _logger.LogInformation("Notification worker stopping.");
            }

            private async Task ProcessBatchAsync(CancellationToken ct)
            {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var emailProvider = scope.ServiceProvider.GetRequiredService<IEmailProvider>();

                // Step 1 — Fetch Pending items
                var pendingItems = await db.NotificationQueue
                    .Where(n => n.Status == NotificationStatus.Pending &&
                               (n.ScheduledAt == null || n.ScheduledAt <= DateTimeOffset.UtcNow))
                    .OrderBy(n => n.CreatedAt)
                    .Take(_batchSize)
                    .ToListAsync(ct);

                if (!pendingItems.Any())
                    return;

                // Step 2 — Mark them Processing (EF concurrency prevents double pickup)
                foreach (var item in pendingItems)
                {
                    item.Status = NotificationStatus.Processing;
                    item.ProcessingStartedAt = DateTimeOffset.UtcNow;
                }

                try
                {
                    await db.SaveChangesAsync(ct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Another worker grabbed these items — skip the batch
                    return;
                }

                // Step 3 — Process each notification
                foreach (var item in pendingItems)
                {
                    if (ct.IsCancellationRequested) break;

                    try
                    {
                        if (item.Channel == NotificationChannel.Email)
                        {
                            await emailProvider.SendEmailAsync(item.Recipient, item.Subject ?? "", item.Body, ct);
                        }
                        else
                        {
                            throw new NotSupportedException($"Channel {item.Channel} is not supported.");
                        }

                        // Mark as sent
                        item.Status = NotificationStatus.Sent;
                        item.ProcessingWorkerId = Guid.NewGuid();
                        await db.SaveChangesAsync(ct);
                    }
                    catch (Exception sendEx)
                    {
                        _logger.LogWarning(sendEx, "Failed to send notification {Id}", item.NotificationQueueId);

                        item.RetryCount += 1;
                        item.FailureReason = sendEx.Message;

                        if (item.RetryCount >= _maxRetries)
                        {
                            db.DeadLetterNotifications.Add(new DeadLetterNotification
                            {
                                NotificationQueueId = item.NotificationQueueId,
                                Recipient = item.Recipient,
                                Subject = item.Subject,
                                Body = item.Body,
                                RetryCount = item.RetryCount,
                                FailureReason = sendEx.ToString(),
                                CreatedAt = DateTimeOffset.UtcNow
                            });

                            db.NotificationQueue.Remove(item);
                        }
                        else
                        {
                            // Reschedule with exponential backoff
                            var delaySeconds = Math.Pow(2, item.RetryCount) * _retryBase.TotalSeconds;
                            var jitter = new Random().NextDouble() * 10;

                            item.ScheduledAt = DateTimeOffset.UtcNow.AddSeconds(delaySeconds + jitter);
                            item.Status = NotificationStatus.Pending;
                            item.ProcessingWorkerId = null;
                            item.ProcessingStartedAt = null;
                        }

                        await db.SaveChangesAsync(ct);
                    }
                }
            }
        }
    }
