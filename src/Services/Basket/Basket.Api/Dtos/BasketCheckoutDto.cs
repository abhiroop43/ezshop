namespace Basket.Api.Dtos;

public class BasketCheckoutDto
{
    public string UserName { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }

    #region "shipping and billing address"

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

    #endregion

    #region "payment"

    public string CardName { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string Expiration { get; set; } = null!;
    public string Cvv { get; set; } = null!;

    #endregion
}
