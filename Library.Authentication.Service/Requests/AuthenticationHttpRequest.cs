using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationHttpRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}