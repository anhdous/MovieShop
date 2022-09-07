using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository
{
    Task<Purchase> AddPurchase(Purchase purchase);
}