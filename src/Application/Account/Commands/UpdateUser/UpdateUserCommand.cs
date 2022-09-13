using GLS.Application.Common.Exceptions;
using GLS.Application.Common.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MediatR;

namespace GLS.Application.Account.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public string Id { get; init; }

    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Password { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public UpdateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //var user = await _userManager.FindByIdAsync(request.Id);

        //if (user == null)
        //{
        //    throw new NotFoundException(nameof(user), request.Id);
        //}

        //user.Email = request.Email;
        //user.PhoneNumber = request.PhoneNumber;
        //await _userManager.UpdateAsync(user);

        return Unit.Value;
    }
}
