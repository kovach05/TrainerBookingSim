using BusinessLogic;

namespace DataAccess.Models;

public class Subscription
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Visits { get; set; }
    public DateTimeOffset  CreatedDate { get; set; }
    public DateTimeOffset  ExpiryDate { get; set; }
    public bool IsExpired { get; set; }
    public string? TrainerId { get; set; }
    public int? ExternalSubscriptionId { get; set; }
    public virtual User User { get; set; }
    public virtual Trainer Trainer { get; set; }
    public int? ClientId { get; set; }
}