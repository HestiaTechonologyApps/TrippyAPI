// File: TrippyAPI/Repositories/Implementations/ExceptionLogRepository.cs
using Trippy.Domain.Entities;
using Trippy.InfraCore.Data;

public class ExceptionLogRepository : IExceptionLogRepository
{
    private readonly AppDbContext _context;
    public ExceptionLogRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(ExceptionLog log)
    {
        await _context.ExceptionLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}