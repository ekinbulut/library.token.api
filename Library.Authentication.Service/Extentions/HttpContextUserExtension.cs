using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using Library.Authentication.Service.ServiceModels;

namespace Library.Authentication.Service.Extentions
{
    [ExcludeFromCodeCoverage]
    public static class HttpContextUserExtension
    {
        public static LoginUser ExctractClaims(this ClaimsPrincipal userClaims)
        {
            var username = userClaims.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Name)?.Value;
            var role = userClaims.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Role)?.Value;
            var userId = userClaims.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Sid)?.Value;

            var user = new LoginUser
                       {
                           Id = int.Parse(userId), Username = username, Information = new UserInformation {Role = role}
                       };

            return user;
        }
    }
}