using System.Net;
using System.Web.Http;
using HealthStream.Api.Models.Authentication;
using HealthStream.Services.Authentication;
using HealthStream.Services.Authentication.Exceptions;

namespace HealthStream.Api.Controllers
{
    [RoutePrefix("authentication")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public IHttpActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _authenticationService.RegisterUser(model.Username, model.Password, model.EmailAddress);
                return Ok();
            }
            catch (UsernameExistsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public IHttpActionResult Authenticate(AuthenticationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _authenticationService.AuthenticateUser(model.Username, model.Password);
            switch (response.Result)
            {
                case AuthenticationResult.Success:
                    return Ok(response.Token);
                case AuthenticationResult.InvalidUsernameOrPassword:
                    return Unauthorized();
                case AuthenticationResult.AccountLocked:
                    return BadRequest("Your account has been locked out.");
            }

            return InternalServerError();
        }
    }
}
