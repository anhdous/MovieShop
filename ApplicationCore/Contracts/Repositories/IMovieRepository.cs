using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    // CRUD methods
    // get top 30 grossing movies from database
    List<Movie> GetTop30GrossingMovies();
    // Get Movie By Id
    // Get Movie By Genre
}