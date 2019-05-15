using System;
using Library.Authentication.Service.Data.Repositories;
using Library.Authentication.Service.Responses.Authentication;
using Library.Authentication.Service.ServiceModels;

namespace Library.Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(ITokenManager tokenManager, IUserRepository userRepository)
        {
            _tokenManager = tokenManager;
            _userRepository = userRepository;
        }

        public AuthenticationServiceResponse AuthenticateUser(string username, string password)
        {
            var dbrequest = _userRepository.GetUser(username, password);

            if (dbrequest == null) throw new ArgumentNullException("Db request returns NULL", new Exception());

            var user = new LoginUser
                       {
                           Id = dbrequest.Id, Username = dbrequest.Username, Information = new UserInformation
                                                                                           {
                                                                                               Role = dbrequest.Role
                                                                                                   .Name
                                                                                               , Permissions = null
                                                                                           }
                       };

            user.Token = _tokenManager.GenerateToken(user);

            return new AuthenticationServiceResponse {UserInformation = user};
        }
    }
}