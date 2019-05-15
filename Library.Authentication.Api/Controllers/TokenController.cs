using System.Diagnostics.CodeAnalysis;
using System.Net;
using Library.Authentication.Service;
using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Authentication.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Authorize]
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _authService;

        public TokenController(ITokenService authService)
        {
            _authService = authService;
        }

        // POST api/values
        [AllowAnonymous]
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Post([FromBody] AuthenticationHttpRequest request)
        {
            var user = _authService.GetToken(request.Username, request.Password);

            if (user == null) return BadRequest(new {message = "Username or password is incorrect"});

            var response = new AuthenticationHttpResponse {Token = user.UserInformation.Token};

            return StatusCode((int) HttpStatusCode.OK, response);
        }
    }
}