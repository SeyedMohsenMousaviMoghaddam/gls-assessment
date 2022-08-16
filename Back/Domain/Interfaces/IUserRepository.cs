using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetPopularDevelopers(int count);
        Task<User> GetByUserName(string username);
    }
}
