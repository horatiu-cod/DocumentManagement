using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Infrastructure.DataAccess.DocumentManagementContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository
{
    public class DocumentRepository(DocumentManagementDbContext context) : IDocumentRepository
    {
        private readonly DocumentManagementDbContext _context = context;

        public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Documents.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Document?> GetByIdWithSignaturesAndEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Documents.Include(x => x.Employee).Include(x => x.Signatures).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Document>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Documents.Include(x => x.Employee).Include(x => x.Signatures).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Document entity, CancellationToken cancellationToken)
        {
            await _context.Documents.AddAsync(entity, cancellationToken);
        }

        public async Task<Document?> GetByIdWithSignaturesAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Documents.Include(x => x.Signatures).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Document?> GetByIdWithEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Documents.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task UpdateAsync(Document entity, CancellationToken cancellationToken)
        {
            // Attach the entity to the context if it's not already tracked
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Documents.Attach(entity);
            }
            // Mark the entity as modified
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Documents.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
            if (entity != null)
            {
                _context.Documents.Remove(entity);
            }
        }
    }
}
