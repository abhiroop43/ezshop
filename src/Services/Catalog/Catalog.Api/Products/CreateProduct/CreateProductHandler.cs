namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("{PrpoertyName} is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("{PrpoertyName} is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("{PrpoertyName} is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PrpoertyName} must be greater than 0");
    }
}

internal class CreateProductCommandHandler(
    IDocumentSession session,
    IValidator<CreateProductCommand> validator
) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await validator.ValidateAsync(command, cancellationToken);

        if (result.Errors.Count != 0)
        {
            throw new ValidationException(result.Errors);
        }

        // create product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        // save to db
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // return create product result
        return new CreateProductResult(product.Id);
    }
}
