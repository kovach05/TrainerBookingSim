namespace DataAccess.Models;

public class Booking
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int TrainerId { get; set; }
    public DateTimeOffset BookingDate { get; set; }
    public DateTime CreatedDate { get; set; }
}