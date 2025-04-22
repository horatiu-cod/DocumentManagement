using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Signatures;
using DocumentManagement.Infrastructure.DataAccess.SignaturesContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository
{
    public class SignatureRepository(SignatureDbContext context) : ISignatureRepository
    {
        private readonly SignatureDbContext _context = context;

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
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Signatures.FindAsync(id);
            if (entity != null)
            {
                _context.Signatures.Remove(entity);
            }
        }
    }
}
