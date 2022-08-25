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
        public async Task<ServerResult> GetAll([FromBody] DatatableRequestVM model)
        {
            return await _service.Get(model);
        }

        [HttpGet("{id}")]
        public async Task<ServerResult> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("{id}")]
        public async Task<ServerResult> GetAllLog(int id)
        {
            return await _service.GetLog(id);
        }
        //[HttpPost]
        //public async Task<ServerResult> GetLookup([FromBody] LookupDTO item)
        //{
        //    return await _service.GetLookup(item);
        //}


        [HttpDelete("{id}")]
        public async Task<ServerResult> Delete(int id)
        {
            return await _service.Delete(id);
        }

        [HttpPost]
        public async Task<ServerResult> Save([FromBody] UserVM item)
        {
            return await _service.Save(item, UserId);
        }


        [HttpPost]
        public async Task<ServerResult> ChangePassword([FromBody] ChangePasswordVM item)
        {
            item.UserId = UserId;
            return await _service.ChangePassword(item, UserId);
        }

        [HttpPost]
        public async Task<ServerResult> ChangePasswordAllUser([FromBody] ChangePasswordVM item)
        {
            return await _service.ChangePassword(item, UserId);
        }

        #endregion

        #region Role

        [HttpGet("{id}")]
        public async Task<ServerResult> GetRoleByUserId(int id)
        {
            return await _service.GetRoleByUserId(id);
        }

        [HttpPost]
        public async Task<ServerResult> AddRoleForUser([FromBody]int id)
        {
            return await _service.AddRoleForUser(id, UserId);
        }

        [HttpGet("{id}")]
        public async Task<ServerResult> RemoveRoleFromUser(int id)
        {
            return await _service.RemoveRoleFromUser(id, UserId);
        }

        #endregion

    }
}