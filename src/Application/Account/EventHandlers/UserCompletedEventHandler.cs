using GLS.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GLS.Application.Account.EventHandlers;

public class UserCompletedEventHandler : INotificationHandler<UserCompletedEvent>
{
    private readonly ILogger<UserCompletedEventHandler> _logger;
    public UserCompletedEventHandler(ILogger<UserCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
