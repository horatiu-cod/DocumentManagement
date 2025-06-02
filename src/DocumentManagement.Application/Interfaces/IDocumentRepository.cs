using DocumentManagement.Domain.Entities.Documents;

namespace DocumentManagement.Application.Interfaces;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Document?> GetByIdWithEmployeeAsync(Guid id, CancellationToken cancellationToken);
    Task<Document?> GetByIdWithSignaturesAsync(Guid id, CancellationToken cancellationToken);
    Task<Document?> GetByIdWithSignaturesAndEmployeeAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Document>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Document entity, CancellationToken cancellationToken);
    Task UpdateAsync(Document entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
