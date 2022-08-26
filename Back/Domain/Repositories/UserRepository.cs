using DAL.DataBaseContexts;
using DAL.Interfaces;
using DAL.Models;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GLSTablesDataBaseContext context) : base(context)
        {
        }
        public async Task<User> GetByUserName(string username)
        {
            return _context.Set<User>().FirstOrDefault(p=>p.UserName == username);
        }
    }
}
