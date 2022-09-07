using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<User> AddUser(User user);
    Task<User> GetAllPurchasesForUser(int userId);
    Task<Purchase> GetPurchasesDetails(int userId, int movieId);
}