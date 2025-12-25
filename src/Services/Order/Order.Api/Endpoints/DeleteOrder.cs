using Order.Application.Orders.Commands.DeleteOrder;

namespace Order.Api.Endpoints;

public class DeleteOrder : ICarterModule
{
    // accept a DeleteOrderRequest object
    // map the request to a DeleteOrderCommand
    // use MediatR to send the command to the corresponding handler
    // return a success or not found response
    public record DeleteOrderResponse(bool IsSuccess);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteOrderCommand(id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Order")
        .WithDescription("Delete an order");
    }
}