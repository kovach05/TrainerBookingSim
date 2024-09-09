using DataAccess.Models;

namespace BusinessLogic;

public class Trainer : User
{
    public int Price { get; set; }
    public int MaxTicket { get; set; }
    public int Popular { get; set; }
    public int OccupiedPlaces { get; set; }
    public string? ExternalTrainerId { get; set; }
    

    public Trainer() { }

    public int FreePlaces => MaxTicket - OccupiedPlaces;

    public bool OccupyPlace()
    {
        if (OccupiedPlaces < MaxTicket)
        {
            OccupiedPlaces++;
            return true;
        }
        return false;
    }
}