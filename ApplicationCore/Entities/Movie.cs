namespace ApplicationCore.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Tagline { get; set; } = null!;
    public decimal? Budger { get; set; }
    public decimal? Revenue { get; set; }
    public string ImdbUrl { get; set; } = null!;
    public string TmdbUrl { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string BackdropUrl { get; set; } = null!;
    public string OriginalLanguage { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }

}