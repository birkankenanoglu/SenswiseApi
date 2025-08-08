using Senswise.Domain.Entities;
using System.Linq.Expressions;

namespace Senswise.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}