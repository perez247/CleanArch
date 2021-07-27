using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseClasses
{
    /// <summary>
    /// Base information that should be recored
    /// </summary>
    public class BaseUpdate
    {
        /// <summary>
        /// Date the object was created 
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Date this class was modified
        /// </summary>
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
