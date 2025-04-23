using DocumentManagement.Domain.Entities.Documents;

namespace DocumentManagement.Application.Interfaces;

public interface IDocumentRepository
{
    Task<DocumentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<DocumentEntity?> GetByIdWithEmployeeAsync(Guid id, CancellationToken cancellationToken);
    Task<DocumentEntity?> GetByIdWithSignaturesAsync(Guid id, CancellationToken cancellationToken);
    Task<DocumentEntity?> GetByIdWithSignaturesAndEmployeeAsync(Guid id, CancellationToken cancellationToken);
    Task<List<DocumentEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DocumentEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(DocumentEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
