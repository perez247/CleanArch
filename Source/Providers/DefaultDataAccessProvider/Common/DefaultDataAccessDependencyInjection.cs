using Application.Interfaces.IRepositories.DefaultDataAccess;
using DefaultDataAccessProvider.Repositories;
using DefaultDataAccessProvider.Repositories.DefaultDataAccessUserSection;
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

            // Authentication repository
            services.AddScoped<IDefaultDataAccessAuthRepository, DefaultDataAccessAuthRepostory>();

            // User repository
            services.AddScoped<IDefaultDataAccessUserRepository, DefaultDataAccessUserRepository>();
        }
    }
}