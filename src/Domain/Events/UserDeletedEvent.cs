using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Domain.Events;

public class UserDeletedEvent : BaseEvent
{
    public UserDeletedEvent(IdentityUser item)
    {
        Item = item;
    }

    public IdentityUser Item { get; }
}
