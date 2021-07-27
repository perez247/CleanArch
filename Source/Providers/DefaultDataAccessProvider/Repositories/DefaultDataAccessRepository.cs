using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultDataAccessProvider.Repositories
{
    /// <summary>
    /// Default report
    /// </summary>
    public class DefaultDataAccessRepository : IDefaultDataAccessRepository
    {
        private IApplicationBuilder _app { get; set; }

        private DefaultDataAccessContext _context { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="app"></param>
        public DefaultDataAccessRepository(IApplicationBuilder app)
        {
            _app = app;
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            _context = serviceProvider.GetService<DefaultDataAccessContext>();
        }

        /// <summary>
        /// Save all changes made to the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Complete()
        {
            if (await _context.SaveChangesAsync() <= 0)
                throw new CustomMessageException("It seems we are having issues saving at the moment");

            return true;
        }

        /// <summary>
        /// Overrides the dispose method
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
