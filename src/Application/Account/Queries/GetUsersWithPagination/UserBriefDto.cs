using GLS.Application.Common.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Queries.GetUsersWithPagination;

public class UserBriefDto : IMapFrom<IdentityUser>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
