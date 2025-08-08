using Senswise.Application;
using Senswise.Application.Interfaces.Repositories;
using Senswise.Domain.Entities;

namespace Senswise.Infra
{
    public class CustomerCommandHandler
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(CreateCustomerCommand command)
        {
            var user = new Customer
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Address = command.Address
            };

            await _customerRepository.AddAsync(user);
            return user.Id;
        }

        public async Task HandleDeleteById(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> Handle(UpdateCustomerCommand command)
        {
            var existingUser = await _customerRepository.GetByIdAsync(command.Id);
            if (existingUser == null)
                return false;

            existingUser.FirstName = command.FirstName;
            existingUser.LastName = command.LastName;
            existingUser.Email = command.Email;
            existingUser.Address = command.Address;

            await _customerRepository.UpdateAsync(existingUser);
            return true;
        }
    }
}
