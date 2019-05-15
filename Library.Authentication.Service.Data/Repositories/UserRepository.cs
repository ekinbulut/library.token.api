using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Library.Authentication.Service.Data.Entities;
using Library.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Authentication.Service.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : BaseRepository<EUser>, IUserRepository
    {
        public UserRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public EUser GetUser(string username, string password)
        {
            return Dbcontext.Set<EUser>().SingleOrDefault(p => p.Username == username && p.Password == password);
        }
    }
}