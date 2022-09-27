using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task<List<GenreModel>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAllGenres();
        var genresModels = genres.Select(g => new GenreModel
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();
        return genresModels;
    }

    public async Task<bool> AddGenre(Genre genre)
    {
        var addedGenre = await _genreRepository.Add(genre);
        if (addedGenre.Id > 0)
        {
            return true;
        }

        return false;
    }
    
}