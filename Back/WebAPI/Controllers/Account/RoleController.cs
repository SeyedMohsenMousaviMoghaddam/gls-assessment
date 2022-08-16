using DAL.Infra;
using DAL.IService.Account;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers.Common.Account
{
	public class RoleController : ApiController
	{
		private readonly IRoleService _service;

		public RoleController(IRoleService roleService)
		{
			_service = roleService;
		}

		//[HttpPost]
		//public async Task<ServerResult> GetAll([FromBody]DatatableRequest model)
		//{
		//	return await _service.Get(model);
		//}

		[HttpGet("{id}")]
		public async Task<ServerResult> GetById(int id)
		{
			return await _service.GetById(id);
		}

		//[HttpPost]
		//public async Task<ServerResult> GetLookup([FromBody]LookupDTO item)
		//{
		//	return await _service.GetLookup(item);
		//}

		[HttpDelete("{id}")]
		public async Task<ServerResult> Delete(int id)
		{
			return await _service.Delete(id);
		}

		[HttpPost]
		public async Task<ServerResult> Save([FromBody]RoleVM item)
		{
			return await _service.Save(item, this.UserId);
		}   

    }
}