using System;
using DefaultDataAccessProvider.Repositories;
using Domain.UserSection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DefaultDataAccessProvider.Common
{
    /// <summary>
    /// Configurations for the default database
    /// </summary>
    public static class DefaultDataAccessConfiguration
    {
        /// <summary>
        /// An extension for configuring the connection string and the right database handler
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void ConfigureDefaultDataAccessDatabaseConnections(this IServiceCollection services, string connectionString)
        {
            // throw new Exception(typeof(DefaultDataAccessContext).Assembly.FullName);

            services.AddDbContext<DefaultDataAccessContext>(x => x.UseNpgsql(
                    connectionString, b => b.MigrationsAssembly("Api")
            ));

            // COnfigure identity for the default database
            services.ConfigureDefaultDataAccesstIdentity();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDefaultDataAccesstIdentity(this IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opts => {
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.MaxFailedAccessAttempts = 10;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opts.User.RequireUniqueEmail = true;
                opts.Lockout.AllowedForNewUsers = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireNonAlphanumeric = false;
            });
            // .AddUserValidator<UniqueEmail<User>>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DefaultDataAccessContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();

            // Add cokkies to the application the only resason im using this is for lockout attempt
            services.AddAuthentication().AddApplicationCookie();
        }

        /// <summary>
        /// It ensures all migrations have been applied to the database
        /// </summary>
        /// <param name="app"></param>
        public static void EnsureDefaultDataAccessDatabaseAndMigrationsExtension(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DefaultDataAccessContext>();
                context.Database.Migrate();
            }
        }
    }
}
