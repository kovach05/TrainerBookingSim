namespace BusinessLogic;

public static class TrainerSelectionStrategies
{
    public static List<Trainer> SelectByPopularity(List<Trainer> trainers)
    {
        return trainers.OrderByDescending(t => t.Popular).ToList();
    }

    public static List<Trainer> SelectByPrice(List<Trainer> trainers)
    {
        return trainers.OrderBy(t => t.Price).ToList();
    }

    public static List<Trainer> SelectByFreePlaces(List<Trainer> trainers)
    {
        return trainers.OrderByDescending(t => t.FreePlaces).ToList();
    }
}