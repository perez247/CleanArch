using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserSection
{
    /// <summary>
    /// User role combination
    /// </summary>
    public class UserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// User
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        /// <value></value>
        public Role Role { get; set; }
    }
}
