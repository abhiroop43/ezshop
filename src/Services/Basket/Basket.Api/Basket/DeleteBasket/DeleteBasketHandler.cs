namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserId) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // delete basket from database and cache
        var isSuccess = await repository.DeleteBasket(request.UserId, cancellationToken);
        return new DeleteBasketResult(isSuccess);
    }
}