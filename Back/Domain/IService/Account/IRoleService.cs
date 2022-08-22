using DAL.Infra;
using DAL.ViewModels;

namespace DAL.IService.Account
{
    public interface IRoleService
    {

        Task<ServerResult> Get(DatatableRequestVM model);
        Task<ServerResult> GetById(int id);
	    //Task<ServerResult> GetLookup(LookupDTO item);
	    Task<ServerResult> Save(RoleVM item, int userId);
	    Task<ServerResult> Delete(int id);

    }
}