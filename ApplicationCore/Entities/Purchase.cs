namespace ApplicationCore.Entities;

public class Purchase
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public Movie Movie { get; set; }
    public User User { get; set; }

    public DateTime PurchaseDateTime { get; set; }
    public string PurchaseNumber { get; set; }
    public decimal TotalPrice { get; set; }

}