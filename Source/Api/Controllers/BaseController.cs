using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// The base controller that initializes the mediator
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IDefaultDataAccessUnitOfWork _unitOfWork;

        /// <summary>
        /// Unit of work
        /// </summary>
        /// <value></value>
        protected IDefaultDataAccessUnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork = (IDefaultDataAccessUnitOfWork)HttpContext.RequestServices.GetService(typeof(IDefaultDataAccessUnitOfWork)));

        /// <summary>
        /// 
        /// </summary>
        /// <param>IMediator</param>
        /// <typeparam>IMediator</typeparam>
        /// <returns></returns>
        protected IMediator Mediator => _mediator ?? (_mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator)));
    }
}
