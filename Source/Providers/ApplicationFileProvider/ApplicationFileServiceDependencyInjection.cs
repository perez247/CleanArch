using Application.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationFileProvider
{
    /// <summary>
    /// Connect sevices
    /// </summary>
    public static class ApplicationFileServiceDependencyInjection
    {
        /// <summary>
        /// Method called to register the services
        /// </summary>
        /// <param name="services"></param>
        public static void AddFileServices(this IServiceCollection services)
        {

            // Add the implementations to be used for DocumentService
            services.AddScoped<IApplicationFileService, ApplicationFileService>();

        }
    }
}