using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Infrastructure.DataAccess.SignaturesContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository;

public class DocumentRepository(SignatureDbContext context) : IDocumentRepository
{
    private readonly SignatureDbContext _context = context;

    public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Documents.Include(x => x.Employee).Include(x => x.Signatures).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Document>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Documents.Include(x => x.Employee).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Document entity)
    {
        await _context.Documents.AddAsync(entity);
    }

    public async Task UpdateAsync(Document entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Documents.FindAsync(id);
        if (entity != null)
        {
            _context.Documents.Remove(entity);
        }
    }
}
