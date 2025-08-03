namespace Catalog.Api.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get All Products")
            .WithDescription("Get All Products");
    }
}