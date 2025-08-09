using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException(nameof(Product), Id)
{
}