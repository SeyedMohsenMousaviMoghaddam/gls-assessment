using GLS.Application.Common.Exceptions;
using GLS.Application.Common.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GLS.Domain.Enums;
using MediatR;

namespace GLS.Application.Account.Commands.UpdateUserDetail;

public record UpdateUserDetailCommand : IRequest
{
    public string Id { get; init; }

    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UpdateUserDetailCommandHandler : IRequestHandler<UpdateUserDetailCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public UpdateUserDetailCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<Unit> Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
    {
        //var user = await _identityService.FindByIdAsync(request.Id);

        //if (user == null)
        //{
        //    throw new NotFoundException(nameof(user), request.Id);
        //}

        //user.Email = request.Email;
        //user.PhoneNumber = request.PhoneNumber;
        //await _identityService.UpdateAsync(user);

        return Unit.Value;
    }
}
