using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using DAL.Infra;
using DAL.Interfaces;
using DAL.IService.Account;
using DAL.ViewModels;
using Serilog;

namespace DAL.Service.Account
{
    public class RoleService : IRoleService
    {
        protected readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ServerResult> Get(DatatableRequestVM model)
        {
            try
            {
                return new ServerResult { Success = true, Data = await _roleRepository.GetAll() };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServerResult> GetById(int id)
        {
            return new ServerResult { Success = true, Data = await _roleRepository.GetById(id) };
        }

        //public async Task<ServerResult> GetLookup(LookupDTO item)
        //{
        //    using (var con = await Connection.GetOpenConnection(true))
        //    {
        //        try
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@Filter", item.Filter, DbType.String);

        //            if (!string.IsNullOrEmpty(item.OrderBy))
        //                parameters.Add("@OrderBy", item.OrderBy, DbType.String);

        //            var output = (await con.QueryAsync<LookupItem>("Lookup_Role", parameters, commandType: CommandType.StoredProcedure))
        //                .ToList();

        //            return new ServerResult { Success = true, Data = output };
        //        }
        //        catch (Exception e)
        //        {
        //            return e.RaiseError();
        //        }
        //    }
        //}

        public async Task<ServerResult> Save(RoleVM item, int userId)
        {
            try
            {
                var role = _mapper.Map<DAL.Models.Role>(item);
                return new ServerResult { Success = true, Data = await _roleRepository.Add(role) };

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<ServerResult> Delete(int id)
        {
            try
            {
                await _roleRepository.Remove(await _roleRepository.GetById(id));
                return new ServerResult { Success = true };
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
