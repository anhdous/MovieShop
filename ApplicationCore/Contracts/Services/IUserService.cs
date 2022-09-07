using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
    Task IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
    Task<List<MovieCardModel>> GetAllPurchasesForUser(int id);

    Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId);
    Task AddFavorite(FavoriteRequestModel favoriteRequest);
    Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
    Task FavoriteExists(int id, int movieId);
    Task GetAllFavoritesForUser(int id);
    Task AddMovieReview(ReviewRequestModel reviewRequest);
    Task UpdateMovieReview(ReviewRequestModel reviewRequest);
    Task DeleteMovieReview(int userId, int movieId);
    Task GetAllReviewsByUser(int id);
    
    
    
}