using ApplicationEmailProvider;
using ApplicationFileProvider;
using DefaultDataAccessProvider.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Common.Extensions
{
    /// <summary>
    /// Application register dependency injection
    /// </summary>
    public static class ApplicationDependencyInjections
    {
        /// <summary>
        /// Method to call all other methods to add the services
        /// </summary>
        /// <param name="services"></param>
        public static void AddAllServices(this IServiceCollection services)
        {
            // Add the unit of work
            services.AddDefaultDataRepositories();

            // // Add the services
            services.AddFileServices();

            // Add the repositories
            services.AddEmailServices();
        }
    }
}