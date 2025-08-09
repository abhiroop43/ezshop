namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        const string requiredErrorMessage = "{PropertyName} is required";
        RuleFor(x => x.Id).NotEmpty().WithMessage(requiredErrorMessage);
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(requiredErrorMessage)
            .Length(2, 150)
            .WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage(requiredErrorMessage);
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage(requiredErrorMessage);
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var existingProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (existingProduct == null) throw new ProductNotFoundException(command.Id);

        existingProduct = command.Adapt(existingProduct);
        session.Update(existingProduct);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}