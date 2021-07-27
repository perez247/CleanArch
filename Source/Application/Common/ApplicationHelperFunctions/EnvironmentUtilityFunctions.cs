using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ApplicationHelperFunctions
{
    /// <summary>
    /// Class that has all the environment data
    /// </summary>
    public static class EnvironemtUtilityFunctions
    {
        /// <summary>
        /// Application hostname
        /// </summary>
        public static readonly string ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// Application hostname
        /// </summary>
        public static readonly string HOSTNAME = Environment.GetEnvironmentVariable("HOSTNAME") ?? "http://localhost";

        /// <summary>
        /// Digital ocean s3 buckets
        /// </summary>
        /// <returns></returns>
        public static readonly string DO_S3_BUCKET = Environment.GetEnvironmentVariable("DO_S3_BUCKET");

        /// <summary>
        /// Authorization token for JWT generation
        /// </summary>
        /// <returns></returns>
        public static readonly string AUTHORIZATION_TOKEN = Environment.GetEnvironmentVariable("AUTHORIZATION_TOKEN") ?? "ThisismenttobetheAuthorizationtoken";

        /// <summary>
        /// Connection string for default data context db
        /// </summary>
        /// <returns></returns>
        public static readonly string DEFAULT_DATA_CONTEXT_CONNECTION_STRING = Environment.GetEnvironmentVariable("DEFAULT_DATA_CONTEXT_CONNECTION_STRING") ?? "host=postgres_image;port=5432;database=marg_db;username=root_user;password=root_password;";

        /// <summary>
        /// Socket Server
        /// </summary>
        /// <returns></returns>
        public static readonly string SOCKET = Environment.GetEnvironmentVariable("SOCKET") ?? "docker_notification_server:8880";

        /// <summary>
        /// Email credential
        /// </summary>
        /// <returns></returns>
        public static readonly string EMAIL_SERVER = Environment.GetEnvironmentVariable("EMAIL_SERVER");
    }
}
