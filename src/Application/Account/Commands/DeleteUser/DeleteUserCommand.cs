using GLS.Application.Common.Exceptions;
using GLS.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Commands.DeleteUser;

public record DeleteUserCommand(string Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public DeleteUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //var user = await _userManager.FindByIdAsync(request.Id);

        //if (user == null)
        //{
        //    throw new NotFoundException(nameof(user), request.Id);
        //}

        //await _userManager.DeleteAsync(user);

        return Unit.Value;
    }
}
