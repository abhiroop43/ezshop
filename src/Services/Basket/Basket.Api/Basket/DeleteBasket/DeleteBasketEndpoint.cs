namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userId}", async (string userId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userId));
                var response = result.Adapt<DeleteBasketResult>();

                return response.IsSuccess ? Results.NoContent() : Results.BadRequest(response);
            })
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket for UserId")
            .WithDescription("Delete Basket for UserId");
    }
}