using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MovieShopDbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options ): base(options)
    {

    }

    // Dbsets are properties of DbContext class

    // Movies Table access
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Trailer> Trailers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
    }
    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
}