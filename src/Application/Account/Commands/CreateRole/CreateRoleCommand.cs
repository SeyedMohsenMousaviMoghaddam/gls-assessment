using GLS.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Commands.CreateRole;

public record CreateRoleCommand : IRequest<string>
{
    public string? Title { get; init; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public CreateRoleCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        //IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(request.Title));
        var role = await _identityService.GetUserNameAsync(request.Title);// _roleManager.FindByNameAsync
        return "";// role.Id;

    }
}
