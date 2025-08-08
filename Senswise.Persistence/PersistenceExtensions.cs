using Microsoft.Extensions.DependencyInjection;
using Senswise.Application.Interfaces.Repositories;
using Senswise.Persistence.Repositories;

namespace Senswise.Persistence
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
