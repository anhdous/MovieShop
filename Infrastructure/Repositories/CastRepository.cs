using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : ICastRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public CastRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<Cast> GetById(int id)
    {
        //select * from cast where id=1 join movie, moviecast
        var castDetails = await _movieShopDbContext.Casts
            .Include(m => m.MoviesOfCast).ThenInclude(m => m.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        return castDetails;
    }
}