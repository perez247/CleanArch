using Application.Common.RequestResponsePipeline;
using Application.Exceptions;
using Application.Interfaces.IExceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Common.ExceptionHandlers
{
    /// <summary>
    /// Intercept Exception before sending back to the server
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApplicationWebExceptionHandler : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="ienv"></param>
        public ApplicationWebExceptionHandler(IWebHostEnvironment ienv)
        {
            _env = ienv;
        }

        /// <summary>
        /// Override default exception event action
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            if (context.Exception is AppFluentValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    new ApplicationErrorResponse()
                    {
                        Errors = ((Application.Exceptions.AppFluentValidationException)context.Exception).Failures,
                        Environment = _env.EnvironmentName.ToLower()
                    },
                    jsonSerializerSettings
                );

                return;
            }

            var code = (int)HttpStatusCode.InternalServerError;
            var exception = context.Exception as IGeneralException;

            if (exception != null)
            {
                code = (int)exception.StatusCode;
            }

            var errorresponse = new ApplicationErrorResponse() { Error = context.Exception.Message, Environment = _env.EnvironmentName.ToLower() };

            errorresponse.StackTrace = !_env.IsProduction() ? context.Exception.StackTrace : null;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = code;
            context.Result = new JsonResult(errorresponse, jsonSerializerSettings);
        }
    }
}
