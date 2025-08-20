using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon =
            await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName.ToUpper() == request.ProductName.ToUpper());

        if (coupon is null)
        {
            logger.LogWarning("No discount found for product: {ProductName}", request.ProductName);
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
        }

        logger.LogInformation("Discount is retrieved for product: {ProductName}, amount: {Amount}", coupon.ProductName,
            coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            logger.LogWarning("Parsed coupon is null");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        }

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount added successfully for product: {ProductName}, amount: {Amount}",
            coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
        {
            logger.LogWarning("Parsed coupon is null");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        }

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount updated successfully for id: {Id}, product: {ProductName}, amount: {Amount}",
            coupon.Id, coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var coupon =
            await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName.ToUpper() == request.ProductName.ToUpper());

        if (coupon is null)
        {
            logger.LogWarning("Could not find a coupon for product: {ProductName}", request.ProductName);
            throw new RpcException(new Status(StatusCode.NotFound, "No coupon found for this product"));
        }

        dbContext.Coupons.Remove(coupon);
        var success = await dbContext.SaveChangesAsync();

        logger.LogInformation("Coupon deletion for product: {ProductName}, success: {Success}", coupon.ProductName,
            success > 0);

        return new DeleteDiscountResponse
        {
            Success = success > 0
        };
    }
}