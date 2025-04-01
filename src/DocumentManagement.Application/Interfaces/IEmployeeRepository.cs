using DocumentManagement.Domain.Entities.Employees;
using System;

namespace DocumentManagement.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Employee entity);
    Task UpdateAsync(Employee entity);
    Task DeleteAsync(Guid id);
}
