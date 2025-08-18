using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userId, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userId, cancellationToken);

        if (!string.IsNullOrEmpty(cachedBasket))
        {
            var deserializedBasket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);

            if (deserializedBasket != null) return deserializedBasket;
        }

        var basket = await repository.GetBasket(userId, cancellationToken);
        await cache.SetStringAsync(userId, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserId, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userId, CancellationToken cancellationToken = default)
    {
        var isSuccess = await repository.DeleteBasket(userId, cancellationToken);
        if (!isSuccess) return false;
        await cache.RemoveAsync(userId, cancellationToken);
        return true;
    }
}