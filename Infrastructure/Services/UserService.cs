using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUserRepository _userRepository;

    public UserService(IPurchaseRepository purchaseRepository, IUserRepository userRepository)
    {
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
    }

    public async Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
    {
        var dbPurchase = new Purchase
        {
            MovieId = purchaseRequest.MovieId,
            UserId = purchaseRequest.UserId,
            PurchaseNumber = purchaseRequest.PurchaseNumber,
            TotalPrice = purchaseRequest.TotalPrice
        };
        var createdPurchase = await _purchaseRepository.AddPurchase(dbPurchase);
        return createdPurchase;
    }

    public Task IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
    {   
        var userDetails = await _userRepository.GetAllPurchasesForUser(id);
        var movieCards = new List<MovieCardModel>();
        foreach (var movie in userDetails.PurchaseMovies)
        {
            movieCards.Add(new MovieCardModel 
            { Id = movie.MovieId, 
                Title = movie.Movie.Title,
                PosterUrl = movie.Movie.PosterUrl });
        }
        return movieCards;

        
    }

    public Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId)
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