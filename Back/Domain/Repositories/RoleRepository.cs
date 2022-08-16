using DAL.DataBaseContexts;
using DAL.Interfaces;
using DAL.Models;


namespace DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(GLSTablesDataBaseContext context) : base(context)
        {
        }
    }
}