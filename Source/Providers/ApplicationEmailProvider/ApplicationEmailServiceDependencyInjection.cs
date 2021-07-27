using Application.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationEmailProvider
{
    /// <summary>
    /// Connect services
    /// </summary>
    public static class ApplicationEmailServiceDependencyInjection
    {
        /// <summary>
        /// Method called to register the services
        /// </summary>
        /// <param name="services"></param>
        public static void AddEmailServices(this IServiceCollection services)
        {

            // Add the implementations to be used for photoservices
            services.AddScoped<IApplicationEmailService, ApplicationEmailService>();


            var service = services.BuildServiceProvider().CreateScope().ServiceProvider;

            var email = service.GetService<IApplicationEmailService>();
        }
    }
}