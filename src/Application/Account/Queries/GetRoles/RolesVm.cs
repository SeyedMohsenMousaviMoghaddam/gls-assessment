namespace GLS.Application.Account.Queries.GetRoles;

public class RolesVm
{
    public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();

    public IList<RoleDto> Lists { get; set; } = new List<RoleDto>();
}
