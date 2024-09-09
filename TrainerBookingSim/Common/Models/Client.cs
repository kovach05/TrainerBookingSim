using DataAccess.Models;

namespace Common.Models;

public class Client : User
{
    public string? ExternalClientId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public PassportId? PassportId { get; set; }
}