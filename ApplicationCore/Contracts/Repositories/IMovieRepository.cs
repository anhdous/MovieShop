using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    // CRUD methods
    // get top 30 grossing movies from database
    Task<List<Movie>> GetTop30GrossingMovies();
    
    // Get Movie By Id
    // Movie GetById(int id);
    
    Task<Movie> GetById(int id); 
    // Get Movie By Genre
    Task<Movie> MoviesByGenre(int id, int pageSize = 30, int pageNumber = 1);
}