using Microsoft.AspNetCore.Mvc;
using GLS.Application.Common.Models;
using GLS.Application.Account.Queries.GetUsersWithPagination;
using GLS.Application.Account.Commands.UpdateUser;
using GLS.Application.Account.Commands.CreateUser;
using GLS.Application.Account.Commands.UpdateUserDetail;
using GLS.Application.Account.Commands.DeleteUser;

namespace GLS.Presentation.Controllers
{
    public class UserManagerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserBriefDto>>> GetUsersWithPagination([FromQuery] GetUsersWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(string id, UpdateUserDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}
