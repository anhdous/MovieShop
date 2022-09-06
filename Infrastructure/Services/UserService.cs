using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    public Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        throw new NotImplementedException();
    }

    public Task IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        throw new NotImplementedException();
    }

    public Task GetAllPurchasesForUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task GetPurchasesDetails(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public Task AddFavorite(FavoriteRequestModel favoriteRequest)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
    {
        throw new NotImplementedException();
    }

    public Task FavoriteExists(int id, int movieId)
    {
        throw new NotImplementedException();
    }

    public Task GetAllFavoritesForUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddMovieReview(ReviewRequestModel reviewRequest)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovieReview(int userId, int movieId)
    {
        throw new NotImplementedException();
    }

    public Task GetAllReviewsByUser(int id)
    {
        throw new NotImplementedException();
    }
}