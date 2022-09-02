using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public MovieRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<Movie> GetById(int id)
    {
        //select * from movie where id=1 join genre, cast, moviegenre, moviecast
        var movieDetails = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            .Include(m=>m.Trailers)
            .FirstOrDefaultAsync(m => m.Id == id);
        return movieDetails;
    }

    public async Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        // get total row count
        var totalMoviesCountOfGenre = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();
        if (totalMoviesCountOfGenre == 0)
        {
            throw new Exception("No Movies found for this genre");
        }
        //get the actual data
        var movies = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(g => g.Movie)
            .OrderByDescending(m => m.Movie.Revenue).Select(m=> new Movie
            {
                Id =m.MovieId,
                PosterUrl = m.Movie.PosterUrl,
                Title = m.Movie.Title
            }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var pagedMovies = new PagedResultSet<Movie>( movies, pageNumber, pageSize, totalMoviesCountOfGenre);
        return pagedMovies;
    }

    public async Task<List<Movie>> GetTop30GrossingMovies()
    {
        //call the database with EF Core and get the data
        // Use MovieShopDbContext and Movie DbSet
        // select Top 30 * from Movies order by Revenue
        // write corresponding LINQ Query

        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }
}