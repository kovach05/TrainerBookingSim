namespace BusinessLogic.Strategy;

public class TrainerByPopularityStrategy : TrainerSelectionStrategyBase
{
    public override List<Trainer> Select(List<Trainer> trainers)
    {
        return trainers.OrderByDescending(t => t.Popular).ToList();
    }
}