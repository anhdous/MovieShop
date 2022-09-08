namespace ApplicationCore.Models;

public class PurchaseRequestModel
{
    public Guid PurchaseNumber => Guid.NewGuid();
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? PurchaseDateTime { get; }
}