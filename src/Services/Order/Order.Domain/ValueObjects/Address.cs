namespace Order.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string? EmailAddress { get; } = null!;
    public string AddressLine { get; } = null!;
    public string Country { get; } = null!;
    public string State { get; } = null!;
    public string ZipCode { get; } = null!;

    protected Address() { }

    private Address(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string state,
        string zipCode
    )
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string state,
        string zipCode
    )
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new DomainException("First name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException("Last name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(addressLine))
        {
            throw new DomainException("Address line cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException("Country cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            throw new DomainException("State cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new DomainException("Zip code cannot be empty.");
        }

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
    }
}
