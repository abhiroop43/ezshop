namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(Guid Id);

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("No {0} found with Id {1}", nameof(product), command.Id);
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete(product);

        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(product.Id);
    }
}