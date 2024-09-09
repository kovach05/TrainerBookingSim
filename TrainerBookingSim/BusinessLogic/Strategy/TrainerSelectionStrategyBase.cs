namespace BusinessLogic;

public abstract class TrainerSelectionStrategyBase
{
    public abstract List<Trainer> Select(List<Trainer> trainers);
}