using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trippy.Domain.Entities;


public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repo;
    public AuditLogService(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task AddLogAsync(AuditLog log)
    {
        await _repo.AddAsync(log);
    }

    public async Task<List<AuditLog>> GetAllLogsAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<AuditLog> GetLogByIdAsync(Guid logId)
    {
        return await _repo.GetByIdAsync(logId);
    }
}



