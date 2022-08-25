using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Genre
{
    public int Id { get; set; }
    [MaxLength(64)]
    public string Name { get; set; }

    public ICollection<MovieGenre> { get; set; }
}