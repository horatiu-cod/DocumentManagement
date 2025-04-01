using DocumentManagement.Domain.Entities.Documents;

namespace DocumentManagement.Application.Interfaces;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Document>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Document entity);
    Task UpdateAsync(Document entity);
    Task DeleteAsync(Guid id);
}
