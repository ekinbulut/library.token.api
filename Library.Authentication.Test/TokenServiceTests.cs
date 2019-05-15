using Library.Authentication.Service;
using Library.Authentication.Service.Data.Entities;
using Library.Authentication.Service.Data.Repositories;
using Library.Authentication.Service.ServiceModels;
using Moq;
using Xunit;

namespace Library.Authentication.Test
{
    public class TokenServiceTests
    {
        public TokenServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            var tokenManagerMock = new Mock<ITokenManager>();

            tokenManagerMock.Setup(t => t.GenerateToken(It.IsAny<LoginUserModel>())).Returns(() => "");

            _sut = new TokenService(tokenManagerMock.Object, _userRepository.Object);
        }

        private readonly Mock<IUserRepository> _userRepository;
        private readonly ITokenService _sut;

        [Fact]
        public void WhenAuthenticate_IsSuccessful()
        {
            var user = new EUser
                       {
                           Id = 1, Username = "test", Role = new ERole {Name = "administrator"}
                       };

            _userRepository.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            var response = _sut.GetToken("test", "test");

            Assert.Equal(user.Id, response.UserInformation.Id);
            Assert.Equal(user.Username, response.UserInformation.Username);
            Assert.Equal(user.Role.Name, response.UserInformation.Information.Role);
            Assert.Null(response.UserInformation.Password);
        }

        [Fact]
        public void When_DbRequest_IsNull_ReturnNull()
        {
            _userRepository.Setup(r => r.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(() => null);

            var actual = _sut.GetToken("username", "password");
            
            Assert.Null(actual);
        }
    }
}