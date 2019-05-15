using Library.Authentication.Service.Responses.Authentication;

namespace Library.Authentication.Service
{
    public interface ITokenService
    {
        TokenServiceResponse GetToken(string username, string password);
    }
}