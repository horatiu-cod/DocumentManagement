using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Employees;
using DocumentManagement.Infrastructure.DataAccess.SignaturesContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository
{
    public sealed class EmployeeRepository(SignatureDbContext context) : IEmployeeRepository
    {
        private readonly SignatureDbContext _context = context;
        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Employees.FindAsync(id, cancellationToken);
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity != null)
            {
                _context.Employees.Remove(entity);
            }
        }
    }
}
