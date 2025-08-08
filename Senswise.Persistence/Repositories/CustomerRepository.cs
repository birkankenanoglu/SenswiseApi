using Microsoft.EntityFrameworkCore;
using Senswise.Application.Interfaces.Repositories;
using Senswise.Domain.Entities;

namespace Senswise.Persistence.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        readonly ApplicationDbContext _dbContext;
        public CustomerRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        DbSet<Customer> Table => _dbContext.Set<Customer>();

        public async Task AddAsync(Customer entity)
        {
            await Table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await Table.FindAsync(id);
            if (customer != null)
            {
                Table.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task UpdateAsync(Customer entity)
        {
            Table.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
