using System;
using Library.Authentication.Service.Data.Repositories;
using Library.Authentication.Service.Responses.Authentication;
using Library.Authentication.Service.ServiceModels;

namespace Library.Authentication.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IUserRepository _userRepository;

        public TokenService(ITokenManager tokenManager, IUserRepository userRepository)
        {
            _tokenManager = tokenManager;
            _userRepository = userRepository;
        }

        public TokenServiceResponse GetToken(string username, string password)
        {
            var dbrequest = _userRepository.GetUser(username, password);

            if (dbrequest == null) return null;

            var user = new LoginUserModel
                       {
                           Id = dbrequest.Id, Username = dbrequest.Username, Information = new UserInformation
                                                                                           {
                                                                                               Role = dbrequest.Role
                                                                                                   .Name
                                                                                               , Permissions = null
                                                                                           }
                       };

            user.Token = _tokenManager.GenerateToken(user);

            return new TokenServiceResponse {UserInformation = user};
        }
    }
}