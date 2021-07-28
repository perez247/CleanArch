using Application.Common.GenericDtos.UserDtoSection;
using Application.Common.RequestResponsePipeline;
using Application.Entities.Authentication.Query.SignInUser;
using Application.Entities.Authentication.Command.SignUpIndividual;
using Application.Entities.Authentication.Command.SignUpOrganization;
using Application.Entities.Authentication.Command.VerifyEmailAddress;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Application.Entities.Authentication.Query.ResendConfirmEmail;
using Application.Entities.Authentication.Query.SendForgotPasswordLink;
using Application.Entities.Authentication.Command.ResetUserPassword;

namespace Api.Controllers
{
    /// <summary>
    /// Authentication Api for verifying/Authenticating users
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Hosting environment variable
        /// </summary>
        private readonly IWebHostEnvironment _ienv;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ienv"></param>
        public AuthController(IWebHostEnvironment ienv)
        {
            _ienv = ienv;
        }

        /// <summary>
        /// Sign up an individual to the platform
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Signed up individual with email sent</response>
        /// <response code="200">UserId and encoded token in DEVELOPMENT</response>
        /// <response code="400">Failed to sign up individual with error message</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignUpUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApplicationBlankResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpPost("signup-individual")]
        public async Task<IActionResult> RegisterIndividual([CustomizeValidator(Skip = true)] SignUpIndividualCommand command)
        {
            var result = await Mediator.Send(command);
            
            if (_ienv.IsDevelopment())
                return Ok(result);

            return Ok();
            // return Ok(command);
        }

        /// <summary>
        /// Sign up a organization to the platform
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Signed up organization with email sent</response>
        /// <response code="200">UserId and encoded token in DEVELOPMENT</response>
        /// <response code="400">Failed to sign up organization with error message</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignUpUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApplicationBlankResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpPost("signup-organization")]
        public async Task<IActionResult> RegisterOrganization([CustomizeValidator(Skip = true)] SignUpOrganizationCommand command)
        {
            var result = await Mediator.Send(command);

            if (_ienv.IsDevelopment())
                return Ok(result);

            return Ok();
            // return Ok(command);
        }

        /// <summary>
        /// Sign in a user to the platform
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">data returned back</response>
        /// <response code="400">Invalid token data</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignInUserDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SigninUser([CustomizeValidator(Skip = true)] SignInUserCommand command)
        {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }

        /// <summary>
        /// Verifies user email address
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">If successful or not</response>
        /// <response code="400">Email has already been verified</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(VerifyemailAddressDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmailAddress(VerifyEmailAddressCommand command)
        {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }

        /// <summary>
        /// Send another verification token to email address to confirm email
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Email sent</response>
        /// <response code="400">User has already been verified or email/username not found</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignUpUserDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpGet("resend-confirm-email")]
        public async Task<IActionResult> ResendVerificationEmailAddressToken([FromQuery] ResendConfirmEmailCommand command)
        {
            var result = await Mediator.Send(command);

            if (_ienv.IsDevelopment())
                return Ok(result);

            return Ok();
            // return Ok(command);
        }

        /// <summary>
        /// Request forgot password be sent to user's email
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Token sent to user's email address</response>
        /// <response code="200">UserId and encoded token in DEVELOPMENT</response>
        /// <response code="400">Failed to send with error message</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignUpUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApplicationBlankResponse), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpGet("send-forgot-password-link")]
        public async Task<IActionResult> SendForgotPasswordLink([FromQuery]SendForgotPasswordLinkCommand command)
        {
            var result = await Mediator.Send(command);
            
            if (_ienv.IsDevelopment())
                return Ok(result);

            return Ok();
            // return Ok(command);
        }

        /// <summary>
        /// User reset's password
        /// </summary>
        /// <remarks>Good!</remarks>
        /// <response code="200">Status of the operation</response>
        /// <response code="400">Failed to reset with error message</response>
        [ProducesResponseType(typeof(ApplicationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResetUserPasswordDto), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpPost("reset-user-password")]
        public async Task<IActionResult> ResetUserPassword([FromBody] ResetUserPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
            // return Ok(command);
        }
    }
}
