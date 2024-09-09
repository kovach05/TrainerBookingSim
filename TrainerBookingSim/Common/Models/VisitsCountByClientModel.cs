namespace Common.Models;

public class VisitsCountByClientModel
{
    public int ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int VisitCount { get; set; }
}