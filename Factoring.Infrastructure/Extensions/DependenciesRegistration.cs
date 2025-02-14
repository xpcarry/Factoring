using Factoring.Domain.Contracts;
using Factoring.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Factoring.Infrastructure.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("FactoringDb");
            services.AddDbContext<FactoringDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Factoring.Infrastructure")));
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();

            return services;
        }
    }
}