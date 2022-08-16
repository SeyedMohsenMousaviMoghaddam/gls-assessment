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
    }
}
