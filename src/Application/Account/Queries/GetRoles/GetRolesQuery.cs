using AutoMapper;
using AutoMapper.QueryableExtensions;
using GLS.Application.Common.Interfaces;
using GLS.Application.Common.Security;
using GLS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GLS.Application.Account.Queries.GetRoles;

[Authorize]
public record GetRolesQuery : IRequest<RolesVm>;

public class GetTodosQueryHandler //: IRequestHandler<GetRolesQuery, RolesVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    //{
    //    return new RolesVm
    //    {
    //        PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
    //            .Cast<PriorityLevel>()
    //            .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
    //            .ToList(),

    //        Lists = await _context.TodoLists
    //            .AsNoTracking()
    //            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
    //            .OrderBy(t => t.Title)
    //            .ToListAsync(cancellationToken)
    //    };
    //}
}
