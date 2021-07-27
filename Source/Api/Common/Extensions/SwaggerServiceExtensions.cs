using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Common.Extensions
{
    /// <summary>
    /// Swagger service extension
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Add swagger
        /// </summary>
        /// <param name="services">IServiceCollection class</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APP API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                // c.AddSecurityRequirement(security);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" }
                            }, new List<string>() }
                    });

                // c.DescribeAllEnumsAsStrings();
                // c.DescribeStringEnumsInCamelCase();
                // c.UseReferencedDefinitionsForEnums();

                // Set the comments path for the Swagger JSON and UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// Use Swagger
        /// </summary>
        /// <param name="app">Iapplication builder class</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/docs/swagger/{documentName}/swagger.json";
            });
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/docs/swagger/v1/swagger.json", "APP API V1");
                c.RoutePrefix = "api/docs";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            });

            return app;
        }
    }
}
