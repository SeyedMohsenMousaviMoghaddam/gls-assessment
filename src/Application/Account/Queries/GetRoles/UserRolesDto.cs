using AutoMapper;
using GLS.Application.Common.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Application.Account.Queries.GetRoles;

public class UserRolesDto : IMapFrom<IdentityUser>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }

    public int Priority { get; set; }

    public string? Note { get; set; }

    public void Mapping(Profile profile)
    {
        //profile.CreateMap<IdentityUser, UserRolesDto>()
        //    .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
    }
}
