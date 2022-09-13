using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Domain.Events;

public class UserCompletedEvent : BaseEvent
{
    public UserCompletedEvent(IdentityUser item)
    {
        Item = item;
    }

    public IdentityUser Item { get; }
}
