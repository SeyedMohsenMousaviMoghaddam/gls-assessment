using GLS.Application.Common.Exceptions;
using GLS.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest
{
    public string Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public UpdateRoleCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        //var role = await _roleManager.FindByIdAsync(request.Id);

        //if (role == null)
        //{
        //    throw new NotFoundException(nameof(role), request.Id);
        //}

        //role.Name = request.Title;
        //await _roleManager.UpdateAsync(role);

        return Unit.Value;
    }
}
