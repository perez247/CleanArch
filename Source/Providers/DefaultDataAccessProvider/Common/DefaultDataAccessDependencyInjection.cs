using Application.Interfaces.IRepositories.DefaultDataAccess;
using DefaultDataAccessProvider.Repositories;
using DefaultDatabaseContext.Repositories.DefaultDataAccessAuthSection;
using Microsoft.Extensions.DependencyInjection;

namespace DefaultDataAccessProvider.Common
{
    /// <summary>
    /// Connect sevices
    /// </summary>
    public static class DefaultDataAccessDependencyInjection
    {
        /// <summary>
        /// Method called to register the default data repository
        /// </summary>
        /// <param name="services"></param>
        public static void AddDefaultDataRepositories(this IServiceCollection services)
        {
            // Unit of work for default database
            services.AddScoped<IDefaultDataAccessUnitOfWork, DefaultDataAccessUnitOfWork>();

            // Add Unit of service implementation to contain all repository
            services.AddScoped<IDefaultDataAccessAuthRepository, DefaultDataAccessAuthRepostory>();
        }
    }
}