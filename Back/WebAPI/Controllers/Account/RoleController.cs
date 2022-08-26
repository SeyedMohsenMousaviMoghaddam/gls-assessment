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

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] DatatableRequestVM model)
        {
            return Ok(await _service.Get(model));
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			return Ok(await _service.GetById(id));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await _service.Delete(id));
		}

		[HttpPost]
		public async Task<IActionResult> Save([FromBody]RoleVM item)
		{
			return Ok(await _service.Save(item, this.UserId));
		}   

    }
}