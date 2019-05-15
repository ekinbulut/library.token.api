using Library.Authentication.Service.Responses.Authentication;

namespace Library.Authentication.Service
{
    public interface IAuthenticationService
    {
        AuthenticationServiceResponse AuthenticateUser(string username, string password);
    }
}