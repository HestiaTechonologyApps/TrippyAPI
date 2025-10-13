using Trippy.Domain.DTO;

public interface IAuditRepository
{
    Task LogAuditAsync<T>(
        string tableName,
        string action,
        int? recordId,
        T oldEntity,
        T newEntity,
        string changedBy
    ) where T : class;

    Task<List<AuditLogDTO>> GetAuditLogsForEntityAsync(string tableName, int recordId);

}