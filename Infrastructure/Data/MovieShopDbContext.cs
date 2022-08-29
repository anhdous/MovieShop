using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    {
        // Movies = movies;
        // Genres = genres;
        // MovieGenres = movieGenres;
        // Trailers = trailers;
    }

    // Dbsets are properties of DbContext class

    // Movies Table access
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<Genre> Genres { get; set; }
    //
    // public DbSet<MovieGenre> MovieGenres { get; set; }
    //
    // public DbSet<Trailer> Trailers { get; set; }

    // override the method called OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
    }

    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        //specify all the Fluent API
        builder.HasKey(m => m.Id);
        builder.Property((m => m.Title)).HasMaxLength(512);
        builder.Property(m => m.Overview).HasMaxLength(4096);
        builder.Property(m => m.Tagline).HasMaxLength(512);
        builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
        builder.Property(m => m.UpdatedBy).HasMaxLength(512);
        builder.Property(m => m.CreatedBy).HasMaxLength(512);

        builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

        // you want the property in your C# for your business logic not as column in database
        builder.Ignore(m => m.Rating);

        builder.HasIndex(m => m.Title);
        builder.HasIndex(m => m.Price);
        builder.HasIndex(m => m.Revenue);
        builder.HasIndex(m => m.Budget);

    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> obj)
    {
        throw new NotImplementedException();
    }
    
    
}