namespace Order.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Domain Event {DomainEvent} handled for OrderId: {OrderId}",
            notification.GetType().Name,
            notification.order.Id
        );

        return Task.CompletedTask;
    }
}
