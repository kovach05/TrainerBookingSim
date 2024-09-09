namespace Common.Models;

public class NewClientsByMonthModel
{
    public DateOnly Month { get; set; }
    public int NewClientCount { get; set; }
}