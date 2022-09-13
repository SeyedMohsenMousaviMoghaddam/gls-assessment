using GLS.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public CreateRoleCommandValidator(IApplicationDbContext context, IIdentityService identityService)
    {
        _identityService = identityService;
        _context = context;
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return true;// await _roleManager.FindByNameAsync(title) != null;
    }

}
