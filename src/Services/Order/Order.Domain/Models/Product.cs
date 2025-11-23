namespace Order.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; } = 0;

    public static Product Create(ProductId id, string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Product name cannot be empty.");
        }
        if (price <= 0)
        {
            throw new DomainException("Product price cannot be negative or zero.");
        }

        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price,
        };

        return product;
    }
}
