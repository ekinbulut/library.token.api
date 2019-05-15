using Library.Authentication.Service;
using Library.Authentication.Service.ServiceModels;
using Library.Authentication.Service.Settings;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.TokenManagerTests
{
    public class TokenManagerTest
    {
        public TokenManagerTest()
        {
            var tokenManagerConfigMock = new Mock<IOptions<TokenManagerConfig>>();
            tokenManagerConfigMock.Setup(t => t.Value).Returns(() => new TokenManagerConfig
                                                                     {
                                                                         Secret =
                                                                             "7oj%(w179!13^@n_1tyz)%4hd@g4k)sqk#@=y@)&z-vddiy72x"
                                                                         , Issuer = "issuer@mail.com"
                                                                         , Audience = "audience@mail.com"
                                                                     });

            _tokenManager = new TokenManager(tokenManagerConfigMock.Object);
        }

        private readonly TokenManager _tokenManager;

        [Fact]
        public void WhenTokenGenerateCompleted()
        {
            var actual = _tokenManager.GenerateToken(new LoginUserModel
                                                     {
                                                         Id = 1, Username = "username"
                                                         , Information = new UserInformation {Role = "role"}
                                                     });

            Assert.Contains("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", actual);
        }
    }
}