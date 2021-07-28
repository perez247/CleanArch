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
        private readonly DefaultDataAccessContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public DefaultDataAccessRepository(DefaultDataAccessContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add an item to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task Add<T>(T entity)  where T : class 
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Add list of items to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task AddRangeAsync<T>(ICollection<T> entity)  where T : class 
        {
            await _context.Set<T>().AddRangeAsync(entity);
        }

        /// <summary>
        /// Remove an item from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>(T entity)  where T : class 
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Remove a list of items from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveRange<T>(ICollection<T> entity)  where T : class 
        {
            _context.Set<T>().RemoveRange(entity);
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
