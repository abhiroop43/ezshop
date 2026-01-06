namespace Order.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; } = null!;

    private OrderName(string value)
    {
        Value = value;
    }

    public static OrderName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("OrderName cannot be empty.");
        }

        if (value.Length < DefaultLength)
        {
            throw new DomainException("OrderName should be 5 characters long.");
        }

        return new OrderName(value);
    }
}
