using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MovieShopDbContext _dbContext;
    public UserRepository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
        }

    public async Task<User> GetAllPurchasesForUser(int userId)
    {
        //select * from user where id=1 join movie, purchase
        var userDetails = await _dbContext.Users
            .Include(m => m.PurchaseMovies).ThenInclude(m => m.Movie)
            .FirstOrDefaultAsync(u =>u.Id == userId);
        return userDetails;
    }

    public Task<Purchase> GetPurchasesDetails(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
        }
}