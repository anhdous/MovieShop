using System.Collections.Immutable;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
    {

    }

    // Dbsets are properties of DbContext class

    // Movies Table access
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    // override the method called OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Review>(ConfigureReview);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        modelBuilder.Entity<Favorite>(ConfigureFavorite);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
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

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenres");
        builder.HasKey(mg => new {mg.MovieId, mg.GenreId});
    }
    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCasts");
        builder.HasKey(mc => new {mc.MovieId, mc.CastId});
    }
    private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorites");
        builder.HasKey(f => new { f.MovieId, f.UserId });
    }

    private void ConfigureReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => new {r.MovieId, r.UserId});
        builder.Property(r => r.CreatedDate).HasDefaultValueSql("GetDate()");
        builder.Property(r => r.Rating).HasColumnType("decimal(3,2)").HasDefaultValue(9.9m);

    }
    private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");
        builder.HasKey(p => new {p.MovieId, p.UserId});
        builder.Property(p => p.TotalPrice).HasColumnType("decimal(5,2)").HasDefaultValue(9.9m);

    }
    
    private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(ur => new {ur.RoleId, ur.UserId});
    }



}