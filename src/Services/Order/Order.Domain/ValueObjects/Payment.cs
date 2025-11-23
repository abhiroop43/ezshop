namespace Order.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = null!;
    public string CardNumber { get; } = null!;
    public string Expiration { get; } = null!;
    public string CVV { get; } = null!;
    public int PaymentMethod { get; } = 0;

    protected Payment() { }

    private Payment(
        string? cardName,
        string cardNumber,
        string expiration,
        string cvv,
        int paymentMethod
    )
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(
        string? cardName,
        string cardNumber,
        string expiration,
        string cvv,
        int paymentMethod
    )
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
        {
            throw new DomainException("CardNumber cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(expiration))
        {
            throw new DomainException("Expiration cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(cvv))
        {
            throw new DomainException("CVV cannot be empty.");
        }

        if (cvv.Length != 3)
        {
            throw new DomainException("CVV should be 3 characters long.");
        }

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}
