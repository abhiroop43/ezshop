namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (existingProduct == null)
        {
            logger.LogWarning("{0} with Id {1} was not found", nameof(existingProduct), command.Id);
            throw new ProductNotFoundException(command.Id);
        }

        existingProduct = command.Adapt(existingProduct);
        session.Update(existingProduct);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}