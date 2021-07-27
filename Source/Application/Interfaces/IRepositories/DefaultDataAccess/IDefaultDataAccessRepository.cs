using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories.DefaultDataAccess
{
    /// <summary>
    /// Interface for the default unit of work
    /// </summary>
    public interface IDefaultDataAccessRepository : IDisposable
    {
        /// <summary>
        /// Save all changes made to the database
        /// </summary>
        /// <returns></returns>
        Task<bool> Complete();
    }
}
