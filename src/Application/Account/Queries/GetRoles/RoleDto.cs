using GLS.Application.Common.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Queries.GetRoles;

public class RoleDto : IMapFrom<IdentityRole>
{
    public RoleDto()
    {
        Items = new List<UserRolesDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Colour { get; set; }

    public IList<UserRolesDto> Items { get; set; }
}
