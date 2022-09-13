using GLS.Application.Account.Commands.CreateRole;
using GLS.Application.Account.Commands.DeleteRole;
using GLS.Application.Account.Commands.UpdateRole;
using GLS.Application.Account.Queries.GetRoles;
using Microsoft.AspNetCore.Mvc;


namespace GLS.Presentation.Controllers
{
    public class RoleManagerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<RolesVm>> Get()
        {
            return await Mediator.Send(new GetRolesQuery());
        }


        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateRoleCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdateRoleCommand command)
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
            await Mediator.Send(new DeleteRoleCommand(id));

            return NoContent();
        }
    }
}
