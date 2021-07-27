using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseClasses;

namespace Domain.FileSection
{
    /// <summary>
    /// File data structure for the database
    /// </summary>
    public class AppFile : BaseUpdate
    {
        /// <summary>
        /// Id of the file
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the file if availiable
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Type of this file
        /// </summary>
        /// <value></value>
        public string Type { get; set; }

        /// <summary>
        /// In terms of identifying the file
        /// </summary>
        /// <value></value>
        public string PublicId { get; set; }

        /// <summary>
        /// How to publicly access the file
        /// </summary>
        /// <value></value>
        public string PublicUrl { get; set; }
    }
}
