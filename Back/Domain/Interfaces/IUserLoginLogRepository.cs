using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserLoginLogRepository : IGenericRepository<UserLoginLog>
    {
        Task<IEnumerable<UserLoginLog>> GetLog(int userId);
    }
}
