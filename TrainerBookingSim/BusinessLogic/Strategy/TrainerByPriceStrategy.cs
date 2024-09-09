namespace BusinessLogic.Strategy;

public class TrainerByPriceStrategy : TrainerSelectionStrategyBase
{ 
    public override List<Trainer> Select(List<Trainer> trainers)
    {
        return trainers.OrderBy(t => t.Price).ToList();
    }
}