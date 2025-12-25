using Order.Application.Orders.Queries.GetOrderByName;

namespace Order.Api.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByName : ICarterModule
{
    // accept a GetOrdersByNameRequest object
    // map the request to a GetOrdersByNameQuery
    // use MediatR to send the command to the corresponding handler
    // return a list of orders in the response
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(orderName));
            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders by Name")
        .WithDescription("Gets a list of Orders matching the provided name");
    }
}