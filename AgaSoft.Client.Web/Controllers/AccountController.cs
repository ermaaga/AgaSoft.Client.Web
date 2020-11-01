using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgaSoft.Client.Interfaces;
using AgaSoft.Client.Model.Entities;
using AgaSoft.Client.Providers;
using AgaSoft.Client.Web.ControllerRequest;
using AgaSoft.Client.Web.ControllerResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgaSoft.Client.Web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IAuthenticationProvider _provider;
        public AccountController(IAuthenticationProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// Registrazione utente
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns OK</response>
        /// <response code="401">Ui not autothorize</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ResponseBase> DestroySessions(RegisterRequest request)
        {
            ResponseBase registerResponse = new ResponseBase();
            ActionResult result = null;
            try
            {
                registerResponse.Result = _provider.Register(request.Name, request.LastName, request.Username, request.Email, request.Password, request.Description, out string message);
                registerResponse.Message = message;
                result = Ok(registerResponse);

            }
            catch (Exception ex)
            {
                registerResponse.Message = ex.Message;
                result = StatusCode(500, registerResponse);
            }
            return result;
        }
        /// <summary>
        /// Login utente
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "Username": "Item1",
        ///        "Password": "Item2"
        ///     }
        /// </remarks>
        /// <response code="200">Returns OK</response>
        /// <response code="401">Ui not autothorize</response>
        /// <response code="500">Internal server error</response>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ResponseBase> Login(LoginRequest request)
        {
            ResponseBase registerResponse = new ResponseBase();
            ActionResult result = null;
            try
            {
                registerResponse.Result = _provider.Login(request.Username, request.Password, out string message);
                registerResponse.Message = message;
                result = Ok(registerResponse);

            }
            catch (Exception ex)
            {
                registerResponse.Message = ex.Message;
                result = StatusCode(500, registerResponse);
            }
            return result;
        }

    }
}
