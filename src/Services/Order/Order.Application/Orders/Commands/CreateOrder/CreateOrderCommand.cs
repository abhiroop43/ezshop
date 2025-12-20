using FluentValidation;

namespace Order.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required");
        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("Order Name is required")
            .MaximumLength(200)
            .WithMessage("Order Name cannot exceed 200 characters");
        //RuleFor(x => x.Order.ShippingAddress).NotNull();
        //RuleFor(x => x.Order.BillingAddress).NotNull();
        //RuleFor(x => x.Order.Payment).NotNull();
        RuleFor(x => x.Order.OrderItems)
            .NotEmpty()
            .WithMessage("Order Items should not be empty");
    }
}
