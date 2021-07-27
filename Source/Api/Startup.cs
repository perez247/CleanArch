using Api.Common.ExceptionHandlers;
using Api.Common.Extensions;
using Api.Common.Filters;
using Application.Common.ApplicationHelperFunctions;
using Application.Common.RequestResponsePipeline;
using Application.Entities.Authentication.Command.SignUpIndividual;
using DefaultDataAccessProvider.Common;
using DefaultDataAccessProvider.Seedings;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Api
{
    /// <summary>
    /// Class to initialze the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// Configure property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Environment variable
        /// </summary>
        public IWebHostEnvironment _env { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Connect to the default database
            services.ConfigureDefaultDataAccessDatabaseConnections(EnvironemtUtilityFunctions.DEFAULT_DATA_CONTEXT_CONNECTION_STRING);

            services.AddControllers(options => 
            {
                options.Filters.Add(typeof(ApplicationWebExceptionHandler));
            })
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpIndividualCommand>())
            ;

            // Catch all fluent validator errors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            // Record process time
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            // Record Performance
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

            // Add Services DI
            services.AddAllServices();

            // Add mediator
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, typeof(ApplicationBlankResponse).GetTypeInfo().Assembly);

            // Check for JWT authentication where neccessary
            services.AddApplicationJwtAuthentication();

            // Adds a default in-memory implementation of IDistributedCache
            services.AddMemoryCache(); 

            // Add Swagger Open API
            services.AddSwaggerDocumentation();

            // Allow cors 
            services.AddCors(options =>
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder
                        // .WithOrigins("http://localhost:4200")
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));
            
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerDocumentation();
            }

            app.UseCors("MyPolicy");

            app.EnsureDefaultDataAccessDatabaseAndMigrationsExtension();
            app.SeedDefualtDataContextDatabase();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
