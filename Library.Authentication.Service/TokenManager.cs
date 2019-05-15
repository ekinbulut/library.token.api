using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Authentication.Service.ServiceModels;
using Library.Authentication.Service.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Authentication.Service
{
    public class TokenManager : ITokenManager
    {
        private readonly IOptions<TokenManagerConfig> _tokenManagerConfig;

        public TokenManager(IOptions<TokenManagerConfig> tokenManagerConfig)
        {
            _tokenManagerConfig = tokenManagerConfig;
        }

        public string GenerateToken(LoginUser user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenManagerConfig.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
                                  {
                                      Subject = new ClaimsIdentity(new[]
                                                                   {
                                                                       new Claim(ClaimTypes.Name, user.Username)
                                                                       , new Claim(ClaimTypes.Sid, user.Id.ToString())
                                                                       , new Claim(
                                                                           ClaimTypes.Role, user.Information.Role)
                                                                   })
                                      , Expires = DateTime.UtcNow.AddDays(7), SigningCredentials =
                                          new SigningCredentials(new SymmetricSecurityKey(key)
                                                                 , SecurityAlgorithms.HmacSha256Signature)
                                      , Issuer = _tokenManagerConfig.Value.Issuer
                                      , Audience = _tokenManagerConfig.Value.Audience
                                  };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}