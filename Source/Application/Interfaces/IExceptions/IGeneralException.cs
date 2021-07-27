using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IExceptions
{
    /// <summary>
    /// A generic exception for the web
    /// All applications must inherit this interface exception
    /// </summary>
    public interface IGeneralException
    {
        /// <summary>
        /// Status code
        /// </summary>
        /// <value></value>
        HttpStatusCode StatusCode { get; set; }
    }
}
