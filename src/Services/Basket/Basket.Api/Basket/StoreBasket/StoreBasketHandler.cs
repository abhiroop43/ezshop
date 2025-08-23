using Discount.Grpc;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserId);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("{PropertyName} cannot be null");
        RuleFor(x => x.Cart.UserId).NotEmpty().WithMessage("{PropertyName} is required");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;

        await ApplyDiscount(cart, cancellationToken);

        await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserId);
    }

    private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon =
                await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName },
                    cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}