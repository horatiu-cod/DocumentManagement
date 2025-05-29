using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Employees;
using DocumentManagement.Infrastructure.DataAccess.DocumentManagementContext;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.Repository
{
    public sealed class EmployeeRepository(DocumentManagementDbContext context) : IEmployeeRepository
    {
        private readonly DocumentManagementDbContext _context = context;
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
            // Attach the entity to the context if it's not already tracked
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Employees.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
             await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Employees.FindAsync(id);
            // Check if the entity is not null before attempting to remove it
            if (entity != null)
            {
                _context.Employees.Remove(entity);
            }
        }
    }
}
