namespace BusinessLogic.Strategy;

public class TrainerByFreePlacesStrategy : TrainerSelectionStrategyBase
{
    public override List<Trainer> Select(List<Trainer> trainers)
    {
        return trainers.OrderByDescending(t => t.FreePlaces).ToList();
    }
}