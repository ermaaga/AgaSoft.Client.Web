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
                registerResponse.Result= _provider.Register(request.Email, request.Password, out string message);
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



        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
     
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
