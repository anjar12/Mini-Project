using System;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;

namespace Shared.Repositories
{
	public  static class DepedencyInjection
	{
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IMasterRepositories, MasterRepository>();
            services.AddScoped<ITransactionRepositories, TransactionRepository>();
            // Adding the Unit of work to the DI container
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();

            return services;
        }
    }
}

