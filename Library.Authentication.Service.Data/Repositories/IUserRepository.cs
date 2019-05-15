using Library.Authentication.Service.Data.Entities;
using Library.Common.Data;

namespace Library.Authentication.Service.Data.Repositories
{
    public interface IUserRepository : IRepository<EUser>
    {
        EUser GetUser(string username, string password);
    }
}