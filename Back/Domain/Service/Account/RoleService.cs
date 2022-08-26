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
            return new ServerResult { Success = true, Data = await _roleRepository.GetAll() };
        }

        public async Task<ServerResult> GetById(int id)
        {
            return new ServerResult { Success = true, Data = await _roleRepository.GetById(id) };
        }

        public async Task<ServerResult> Save(RoleVM item, int userId)
        {
            var role = _mapper.Map<DAL.Models.Role>(item);
            if(item.Id == 0)
                return new ServerResult { Success = true, Data = await _roleRepository.Add(role) };
            else
                return new ServerResult { Success = true, Data = await _roleRepository.Update(role) };
        }

        public async Task<ServerResult> Delete(int id)
        {
            await _roleRepository.Remove(await _roleRepository.GetById(id));
            return new ServerResult { Success = true };
        }
    }
}
