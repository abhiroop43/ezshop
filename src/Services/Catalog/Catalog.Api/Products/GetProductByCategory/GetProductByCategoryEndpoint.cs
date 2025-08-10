namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
                async (string category, [AsParameters] GetProductByCategoryRequest request, ISender sender) =>
                {
                    var result =
                        await sender.Send(new GetProductByCategoryQuery(category, request.PageNumber,
                            request.PageSize));

                    var response = result.Adapt<GetProductByCategoryResponse>();
                    return Results.Ok(response);
                })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products by Category")
            .WithDescription("Get Products by Category");
    }
}