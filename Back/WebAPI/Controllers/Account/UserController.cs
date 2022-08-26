using DAL.Infra;
using DAL.IService.Account;
using DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers.Common.Account
{
    public class UserController : ApiController
    {
        #region CTOR and props

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        #endregion

        #region User

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllLog(int id)
        {
            return Ok(await _service.GetLog(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return Ok(await _service.Delete(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] UserVM item)
        {
            UserValidator _validator = new UserValidator();
            var validResult = _validator.Validate(item);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            return Ok(await _service.Save(item, UserId));
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM item)
        {

            item.UserId = UserId;
            ChangePasswordValidator _validator = new ChangePasswordValidator();
            var validResult = _validator.Validate(item);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            return Ok(await _service.ChangePassword(item, UserId));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAllUser([FromBody] ChangePasswordVM item)
        {
            ChangePasswordValidator _validator = new ChangePasswordValidator();
            var validResult = _validator.Validate(item);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            
            return Ok(await _service.ChangePassword(item, UserId));
        }

        #endregion

        #region Role

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByUserId(int id)
        {
            return Ok(await _service.GetRoleByUserId(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleForUser([FromBody]int id)
        {
            return Ok(await _service.AddRoleForUser(id, UserId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RemoveRoleFromUser(int id)
        {
            return Ok(await _service.RemoveRoleFromUser(id, UserId));
        }

        #endregion

    }
}