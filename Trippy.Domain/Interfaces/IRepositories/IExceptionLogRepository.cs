// File: TrippyAPI/Repositories/IExceptionLogRepository.cs
using Trippy.Domain.Entities;

public interface IExceptionLogRepository
{
    Task AddAsync(ExceptionLog log);
}

