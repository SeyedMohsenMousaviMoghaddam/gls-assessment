using DAL.Infra;
using DAL.ViewModels;

namespace DAL.IService.Account
{
    public interface IUserService
    {
        Task<ServerResult> GetByUserName(string username);
        Task<ServerResult> LoginUser(string userName, string password, bool customerAgentLogin = false);

        Task<ServerResult> Get(DatatableRequestVM model);
        Task<ServerResult> GetById(int id);
        Task<ServerResult> Save(UserVM item, int userId);
        Task<ServerResult> ChangePassword(ChangePasswordVM item, int userId);
        Task<ServerResult> Delete(int id);
        Task<ServerResult> GetRoleByUserId(int id);
        Task<ServerResult> AddRoleForUser(int roleId, int userId);
        Task<ServerResult> RemoveRoleFromUser(int roleId, int userId);
        Task<ServerResult> GetLog(int userId);
    }
}