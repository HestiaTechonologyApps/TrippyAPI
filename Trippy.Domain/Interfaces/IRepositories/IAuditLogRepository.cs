using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trippy.Domain.Entities;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog log);
    Task<List<AuditLog>> GetAllAsync();
    Task<AuditLog> GetByIdAsync(Guid logId);
}
