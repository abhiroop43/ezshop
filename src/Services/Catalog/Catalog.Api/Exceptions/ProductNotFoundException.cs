namespace Catalog.Api.Exceptions;

public class ProductNotFoundException(Guid Id) : Exception($"Product with Id {Id} was not found")
{
}