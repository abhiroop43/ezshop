using Order.Application.Orders.Queries.GetOrdersByCustomer;

namespace Order.Api.Endpoints;


public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    // accept a GetOrdersByCustomerRequest object
    // map the request to a GetOrdersByCustomerQuery
    // use MediatR to send the command to the corresponding handler
    // return a list of orders in the response
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders by Customer")
        .WithDescription("Gets a list of Orders for the provided Customer ID");
    }
}