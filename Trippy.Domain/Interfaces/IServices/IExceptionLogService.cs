using Trippy.Domain.Entities;

public interface IExceptionLogService
{
    Task LogExceptionAsync(ExceptionLog log);
}

