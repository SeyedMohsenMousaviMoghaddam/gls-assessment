using GLS.Application.Common.Exceptions;
using GLS.Application.Common.Interfaces;
using GLS.Application.Common.Security;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GLS.Application.Account.Commands.DeleteRole;

public record DeleteRoleCommand(string Id) : IRequest;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public DeleteRoleCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        //var role = await _roleManager.FindByIdAsync(request.Id);

        //if (role == null)
        //{
        //    throw new NotFoundException(nameof(role), request.Id);
        //}

        //await _roleManager.DeleteAsync(role);

        return Unit.Value;
    }
}
