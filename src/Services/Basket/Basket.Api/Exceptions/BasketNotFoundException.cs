namespace Basket.Api.Exceptions;

public class BasketNotFoundException(string userId) : NotFoundException(nameof(ShoppingCart), userId);