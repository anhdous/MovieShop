using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Movie
{
    public int Id { get; set; }
    [MaxLength(256)]
    public string Title { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Tagline { get; set; } = null!;
    public decimal? Budger { get; set; }
    public decimal? Revenue { get; set; }
    public string ImdbUrl { get; set; } = null!;
    public string TmdbUrl { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string BackdropUrl { get; set; } = null!;
    [MaxLength(64)]
    public string OriginalLanguage { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }

    public TYPE Type { get; set; }
    public decimal? CreatedBy { get; set; }
    public decimal? Rating { get; set; }
    

}