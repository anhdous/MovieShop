using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
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