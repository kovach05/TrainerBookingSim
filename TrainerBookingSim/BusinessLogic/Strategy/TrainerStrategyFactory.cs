using BusinessLogic.Strategy;

namespace BusinessLogic;

public class TrainerStrategyFactory
{
    public TrainerSelectionStrategyBase CreateStrategy(string strategyName)
    {
        switch (strategyName.ToLower())
        {
            case "popularity":
                return new TrainerByPopularityStrategy();
            case "price":
                return new TrainerByPriceStrategy();
            case "free_places":
                return new TrainerByFreePlacesStrategy();
            default:
                throw new ArgumentException($"{nameof(strategyName)} was not in allowed range");
        }
    }
}