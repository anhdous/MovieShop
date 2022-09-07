using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly MovieShopDbContext _dbContext;
    public PurchaseRepository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Purchase> AddPurchase(Purchase purchase)
    {
        _dbContext.Purchases.Add(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }


}