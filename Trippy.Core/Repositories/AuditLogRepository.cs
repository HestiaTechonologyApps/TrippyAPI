using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trippy.Domain.Entities;
using Trippy.InfraCore.Data;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly AppDbContext _context;
    public AuditLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuditLog log)
    {
        await _context.AuditLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLogs.ToListAsync();
    }

    public async Task<AuditLog> GetByIdAsync(Guid logId)
    {
        return await _context.AuditLogs.FindAsync(logId);
    }
}
