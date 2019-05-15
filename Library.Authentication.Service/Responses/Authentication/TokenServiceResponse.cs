using Library.Authentication.Service.ServiceModels;

namespace Library.Authentication.Service.Responses.Authentication
{
    public class TokenServiceResponse
    {
        public LoginUserModel UserInformation { get; set; }
    }
}