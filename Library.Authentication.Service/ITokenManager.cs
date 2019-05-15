using Library.Authentication.Service.ServiceModels;

namespace Library.Authentication.Service
{
    public interface ITokenManager
    {
        string GenerateToken(LoginUser user);
    }
}