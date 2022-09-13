using GLS.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Commands.CreateUser;

public record CreateUserCommand : IRequest<string>
{
    public string? UserName { get; init; }
    public string? Email { get; init; }   
    public string? PhoneNumber { get; init; }
    public string? Password { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        //IdentityResult result = await _userManager.CreateAsync(entity,request.Password);
        return "";//entity.Id;
    }
}
