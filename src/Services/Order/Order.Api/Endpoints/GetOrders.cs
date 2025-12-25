using BuildingBlocks.Pagination;
using Order.Application.Orders.Queries.GetOrders;

namespace Order.Api.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    // accept a GetOrdersRequest object
    // map the request to a GetOrdersQuery
    // use MediatR to send the command to the corresponding handler
    // return a list of orders in the response
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Gets a list of Orders with pagination");
    }
}