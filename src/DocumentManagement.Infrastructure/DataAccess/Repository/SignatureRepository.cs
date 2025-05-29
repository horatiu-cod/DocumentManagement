using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Signatures;
using DocumentManagement.Infrastructure.DataAccess.DocumentManagementContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository
{
    public class SignatureRepository(DocumentManagementDbContext context) : ISignatureRepository
    {
        private readonly DocumentManagementDbContext _context = context;

        public async Task<Signature?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Signatures.Include(x => x.Employee).Include(x => x.Document).FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<List<Signature>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Signatures.Include(x => x.Employee).Include(x => x.Document).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Signature entity)
        {
            await _context.Signatures.AddAsync(entity);
        }

        public async Task UpdateAsync(Signature entity)
        {
            // Attach the entity to the context if it's not already tracked
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Signatures.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Signatures.FindAsync(id);
            // Check if the entity is not null before attempting to remove it
            if (entity != null)
            {
                _context.Signatures.Remove(entity);
            }
        }
    }
}
