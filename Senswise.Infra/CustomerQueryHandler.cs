using Senswise.Application.Interfaces.Repositories;
using Senswise.Domain.Entities;

namespace Senswise.Infra
{
    public class CustomerQueryHandler
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }
    }
}
