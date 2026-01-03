using MassTransit;
using Microsoft.FeatureManagement;
using Order.Application.Extensions;

namespace Order.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger
) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Domain Event {DomainEvent} handled for OrderId: {OrderId}",
            domainEvent.GetType().Name,
            domainEvent.order.Id
        );

        if (!await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            return;
        }

        var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
