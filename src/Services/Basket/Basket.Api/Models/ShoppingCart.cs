namespace Basket.Api.Models;

public class ShoppingCart
{
    public ShoppingCart(string userId)
    {
        UserId = userId;
    }

    public ShoppingCart()
    {
    }

    public string UserId { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}