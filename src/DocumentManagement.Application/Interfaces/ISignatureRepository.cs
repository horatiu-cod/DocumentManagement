using DocumentManagement.Domain.Entities.Signatures;

namespace DocumentManagement.Application.Interfaces;

public interface ISignatureRepository
{
    Task<Signature?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Signature>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Signature entity);
    Task UpdateAsync(Signature entity);
    Task DeleteAsync(Guid id);
}
