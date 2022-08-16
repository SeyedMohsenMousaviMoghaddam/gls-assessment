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
        public IEnumerable<User> GetPopularDevelopers(int count)
        {
            return _context.Users.OrderByDescending(d => d.Name).Take(count).ToList();
        }
        public async Task<User> GetByUserName(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(p=>p.UserName == username);
        }
    }
}
