using DAL.Infra;
using DAL.ViewModels;

namespace DAL.IService.Account
{
    public interface IUserService
    {
        Task<ServerResult> GetByUserName(string username);
        Task<ServerResult> LoginUser(string userName, string password, bool customerAgentLogin = false);

        //Task<ServerResult> Get(DatatableRequest model);
        Task<ServerResult> GetById(int id);
        //Task<ServerResult> GetLookup(LookupDTO item);
        Task<ServerResult> Save(UserVM item, int userId);
        //Task<ServerResult> ChangePassword(ChangePasswordCA item, int userId);
        //Task<ServerResult> ChangePassword(ChangePasswordCA item);
        Task<ServerResult> Delete(int id);
        Task<ServerResult> GetRoleByUserId(int id);
        Task<ServerResult> AddRoleForUser(int roleId, int userId);
        Task<ServerResult> RemoveRoleFromUser(int roleId, int userId);
    }
}