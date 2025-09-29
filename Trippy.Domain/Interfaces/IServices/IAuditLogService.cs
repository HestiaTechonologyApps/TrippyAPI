using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trippy.Domain.Entities;

public interface IAuditLogService
{
    Task AddLogAsync(AuditLog log);
    Task<List<AuditLog>> GetAllLogsAsync();
    Task<AuditLog> GetLogByIdAsync(Guid logId);
}
