namespace Order.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event {DomainEvent} handled for OrderId: {OrderId}"
            , notification.GetType().Name
            , notification.order.Id);

        return Task.CompletedTask;
    }
}