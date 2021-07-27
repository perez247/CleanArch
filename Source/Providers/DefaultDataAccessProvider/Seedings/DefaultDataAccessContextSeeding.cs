using Application.Interfaces.IRepositories.DefaultDataAccess;
using DefaultDataAccessProvider.Repositories;
using Domain.UserSection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DefaultDataAccessProvider.Seedings
{
    /// <summary>
    /// Seeding the database with initial data
    /// </summary>
    public static class DefaultDataAccessContextSeeding
    {
        /// <summary>
        /// This seeds the database with initial appropriate data neccessary for the operation of the application
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <returns></returns>
        public static IApplicationBuilder SeedDefualtDataContextDatabase(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            try
            {
                // Get basic requirements
                var env = serviceProvider.GetService<IWebHostEnvironment>();

                // Get the default data context 
                var defaultDataContext = serviceProvider.GetService<DefaultDataAccessContext>();
                var defaultDataContextRoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                var defaultDataContextuserManager = serviceProvider.GetService<UserManager<User>>();
                var defaultDataContextUnitOfWorkRepository = serviceProvider.GetService<IDefaultDataAccessRepository>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return app;
        }
    }
}

