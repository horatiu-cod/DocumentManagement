using DocumentManagement.Domain.Entities.Documents;

namespace DocumentManagement.Application.Interfaces;

public interface IDocumentRepository
{
    Task<DocumentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<DocumentEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DocumentEntity entity);
    Task UpdateAsync(DocumentEntity entity);
    Task DeleteAsync(Guid id);
}
