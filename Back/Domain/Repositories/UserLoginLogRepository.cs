using DAL.DataBaseContexts;
using DAL.Interfaces;
using DAL.Models;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class UserLoginLogRepository : GenericRepository<UserLoginLog>, IUserLoginLogRepository
    {
        public UserLoginLogRepository(GLSTablesDataBaseContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserLoginLog>> GetLog(int userId)
        {
            return _context.Set<UserLoginLog>().Where(p => p.UserId == userId);
        }
    }
}
