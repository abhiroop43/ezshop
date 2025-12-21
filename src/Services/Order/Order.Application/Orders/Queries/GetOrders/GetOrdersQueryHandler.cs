using BuildingBlocks.Pagination;
using Order.Application.Extensions;

namespace Order.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var totalOrders = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);
        var paginatedOrders = new PaginatedResult<OrderDto>(
            query.PaginationRequest.PageIndex,
            query.PaginationRequest.PageSize,
            totalOrders,
            orders.ToOrderDtoList()
        );
        return new GetOrdersResult(paginatedOrders);
    }
}