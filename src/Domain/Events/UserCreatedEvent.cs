using Microsoft.AspNet.Identity.EntityFramework;

namespace GLS.Domain.Events;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(IdentityUser item)
    {
        Item = item;
    }

    public IdentityUser Item { get; }
}
