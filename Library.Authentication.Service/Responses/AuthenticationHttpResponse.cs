using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Responses
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationHttpResponse
    {
        public string Token { get; set; }
    }
}