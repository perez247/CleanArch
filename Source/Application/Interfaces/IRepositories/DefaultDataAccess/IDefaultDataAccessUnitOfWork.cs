namespace Application.Interfaces.IRepositories.DefaultDataAccess
{
    /// <summary>
    /// Default Access Unit of work repository
    /// </summary>
    public interface IDefaultDataAccessUnitOfWork
    {
        /// <summary>
        /// Default Data access context
        /// </summary>
        public IDefaultDataAccessRepository DefaultDataAccessRepository { get; set; }

        /// <summary>
        /// Default Data access authentication priamry for default access but can be used for the application
        /// </summary>
        public IDefaultDataAccessAuthRepository DefaultDataAccessAuthRepository { get; set; }


        /// <summary>
        /// User repository
        /// </summary>
        public IDefaultDataAccessUserRepository DefaultDataAccessUserRepository { get; set; }
    }
}