using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // services will typically expose the business functionality to the UI/client/controllers

    // called by home/index
    // services will always return models/viewmodels/DTO (data transfer objects)
    Task<List<MovieCardModel>> GetTop30GrossingMovies();
    Task<MovieDetailsModel> GetMovieDetails(int movieId);
    Task<PagedResultSet<MovieCardModel>> GetMoviesByPagination(int genreId, int pageSize = 30, int page = 1);
}