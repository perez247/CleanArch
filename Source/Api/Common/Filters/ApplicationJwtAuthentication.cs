using Application.Common.ApplicationHelperFunctions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common.Filters
{
    /// <summary>
    /// Class for validating jwt token
    /// </summary>
    public static class ApplicationJwtAuthentication
    {
        /// <summary>
        /// Add a method inside the IService collection
        /// /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvironemtUtilityFunctions.AUTHORIZATION_TOKEN)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
