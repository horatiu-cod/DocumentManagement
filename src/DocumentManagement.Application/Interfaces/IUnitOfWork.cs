namespace DocumentManagement.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task CommitChangesAsync(CancellationToken cancellationToken = default);
}
