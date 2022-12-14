using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(128)] public string FirstName { get; set; }
    [MaxLength(128)] public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    [MaxLength(256)] public string Email { get; set; }
    [MaxLength(1024)] public string HashedPassword { get; set; }
    [MaxLength(1024)] public string Salt { get; set; }

    [MaxLength(16)] public string? PhoneNumber { get; set; }
    public bool? IsLocked { get; set; }

    [MaxLength(4096)] string? ProfilePictureUrl { get; set; }
    
    public ICollection<Favorite> FavoriteMovies { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Purchase> PurchaseMovies { get; set; }
    public ICollection<UserRole> RolesOfUser { get; set; } = null!;
}