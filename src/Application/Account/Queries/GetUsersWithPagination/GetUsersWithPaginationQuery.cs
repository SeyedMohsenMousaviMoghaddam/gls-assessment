using AutoMapper;
using AutoMapper.QueryableExtensions;
using GLS.Application.Common.Interfaces;
using GLS.Application.Common.Mappings;
using GLS.Application.Common.Models;
using MediatR;

namespace GLS.Application.Account.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler //: IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    //{
    //    return await _context.TodoItems
    //        .Where(x => x.ListId == request.ListId)
    //        .OrderBy(x => x.Title)
    //        .ProjectTo<UserBriefDto>(_mapper.ConfigurationProvider)
    //        .PaginatedListAsync(request.PageNumber, request.PageSize);
    //}
}
