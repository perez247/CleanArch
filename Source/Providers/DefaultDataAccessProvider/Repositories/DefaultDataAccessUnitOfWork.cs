using Application.Interfaces.IRepositories.DefaultDataAccess;
using DefaultDatabaseContext.Repositories.DefaultDataAccessAuthSection;
using Domain.UserSection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DefaultDataAccessProvider.Repositories
{
    /// <summary>
    /// Default access repository
    /// </summary>
    public class DefaultDataAccessUnitOfWork : IDefaultDataAccessUnitOfWork
    {
        private readonly DefaultDataAccessContext _context;
        private readonly UserManager<User> _defaultDataAccessUserManager;
        private readonly SignInManager<User>  _defaultDataAccessSignInManager;

        /// <summary>
        /// Default repository for saving data
        /// </summary>
        /// <returns></returns>
        public IDefaultDataAccessRepository DefaultDataAccessRepository { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        /// <summary>
        /// Default Data access authentication priamry for default access but can be used for the application
        /// </summary>
        /// <returns></returns>
        public IDefaultDataAccessAuthRepository DefaultDataAccessAuthRepository { 
            get => new DefaultDataAccessAuthRepostory(_context, _defaultDataAccessUserManager, _defaultDataAccessSignInManager); 
            set => throw new System.NotImplementedException(); }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Default data access context</param>
        /// <param name="defaultDataAccessUserManager"></param>
        /// <param name="defaultDataAccessSignInManager"></param>
        public DefaultDataAccessUnitOfWork(
            DefaultDataAccessContext context,
            UserManager<User> defaultDataAccessUserManager,
            SignInManager<User>  defaultDataAccessSignInManager
            )
        {
            _context = context;
            _defaultDataAccessUserManager = defaultDataAccessUserManager;
            _defaultDataAccessSignInManager = defaultDataAccessSignInManager;
        }
    }
}