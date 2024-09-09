namespace DataAccess.Models;

public class Visit
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public DateTime VisitDate { get; set; }
}