using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IGenreRepository
{
     Task<List<Genre>> GetAllGenres();
     Task<Genre> Add( Genre genre);
     Task<Genre> Delete( Genre genre);
     Task<Genre> MoviesByGenre(int id, int pageSize, int pageNumber);

}