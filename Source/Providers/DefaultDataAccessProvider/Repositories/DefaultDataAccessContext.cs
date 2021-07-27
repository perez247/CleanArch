using Domain.BaseClasses;
using Domain.ContactSection;
using Domain.FileSection;
using Domain.IndividualSection;
using Domain.UserSection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultDataAccessProvider.Repositories
{
    /// <summary>
    /// Default Data Access Context
    /// </summary>
    public class DefaultDataAccessContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        // ---------------------------------------------------------------------------- User tables

        /// <summary>
        /// Entity Role
        /// </summary>
        /// <value></value>
        public DbSet<UserAdditionalDetail> UserAdditionalDetails { get; set; }

        // ---------------------------------------------------------------------------- Contact tables

        /// <summary>
        /// General contacts
        /// </summary>
        /// <value></value>
        public DbSet<ApplicationContact> ApplicationContacts { get; set; }

        // ---------------------------------------------------------------------------- Individual tables

        /// <summary>
        /// Skills of an individual
        /// </summary>
        /// <value></value>
        public DbSet<IndividualSkill> IndividualSkills { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public DefaultDataAccessContext(DbContextOptions<DefaultDataAccessContext> options)
        : base(options) { }

        /// <summary>
        /// Override savinf to add a couple of actions
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseUpdate> entry in ChangeTracker.Entries<BaseUpdate>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.DateModified =DateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.DateModified = DateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            //await DispatchEvents(cancellationToken);

            return result;
        }

        /// <summary>
        /// Extra configuration on the database
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(DefaultDataAccessContext).Assembly);
            
        }
    }
}
