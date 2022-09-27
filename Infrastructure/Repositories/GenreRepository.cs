using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public GenreRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        var genres = await _movieShopDbContext.Genres.ToListAsync();
        return genres;
    }

    public async Task<Genre> Add(Genre genre)
    {
        _movieShopDbContext.Set<Genre>().Add(genre);
        await _movieShopDbContext.SaveChangesAsync();
        return genre;
    }
    public async Task<Genre> Delete(Genre genre)
    {
        _movieShopDbContext.Set<Genre>().Remove(genre);
        await _movieShopDbContext.SaveChangesAsync();
        return genre;
    }
    public async Task<Genre> MoviesByGenre(int id, int pageSize = 30, int pageNumber = 1)
    {
        var genreDetails = await _movieShopDbContext.Genres
            .Include(m => m.MoviesOfGenre).ThenInclude(m => m.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        return genreDetails;
    }
}