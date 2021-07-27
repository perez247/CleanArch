using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserSection
{
    /// <summary>
    /// Role table for the user
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        /// <summary>
        /// List of roles of the application
        /// </summary>
        /// <value></value>
        public ICollection<UserRole> UserRoles { get; private set; } = new HashSet<UserRole>();
    }
}
