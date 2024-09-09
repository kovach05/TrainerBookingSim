using System.Diagnostics;
using BusinessLogic;
using BusinessLogic.Interface;
using DataAccess;

namespace TrainerBookingSim
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ILogger logger = new NLogger();
            ILoader<Trainer> loader = new ConfigurationJSON(logger);
            List<Trainer> trainers = loader.Load("data.json");

            if (trainers.Count == 0)
            {
                Console.WriteLine("No trainers available.");
                return;
            }

            Console.WriteLine("Enter number of athletes: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfAthletes))
            {
                Console.WriteLine("Invalid number of athletes.");
                return;
            }

            Console.WriteLine("Choose strategy of athletes (popularity, price, free_places): ");
            string strategy = Console.ReadLine().ToLower();

            var strategyFactory = new TrainerStrategyFactory();
            var trainerSelectionStrategy = strategyFactory.CreateStrategy(strategy);

            var selectedTrainers = trainerSelectionStrategy.Select(trainers);

            Console.WriteLine("Available trainers:");
            for (int i = 0; i < selectedTrainers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedTrainers[i].FirstName} - Available Places: {selectedTrainers[i].FreePlaces}");
            }

            Console.WriteLine("Enter the number of the trainer you want to choose:");
            if (!int.TryParse(Console.ReadLine(), out int trainerIndex) || trainerIndex < 1 || trainerIndex > selectedTrainers.Count)
            {
                Console.WriteLine("Invalid trainer number.");
                return;
            }

            Trainer chosenTrainer = selectedTrainers[trainerIndex - 1];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool success = await SimulatePurchasesAsync(selectedTrainers, numberOfAthletes, trainerIndex - 1);

            stopwatch.Stop();

            if (!success)
            {
                Console.WriteLine("Not enough free places for all athletes.");
            }

            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Updated trainer list:");
            foreach (var trainer in selectedTrainers)
            {
                Console.WriteLine($"Trainer Name: {trainer.FirstName}, Available Places: {trainer.FreePlaces}");
            }
        }

        static async Task<bool> SimulatePurchasesAsync(List<Trainer> trainers, int numberOfAthletes, int startIndex)
        {
            var semaphore = new SemaphoreSlim(5);
            var tasks = new List<Task>();
            int currentTrainerIndex = startIndex;
            bool allAthletesAccommodated = true;
            int soldMemberships = 0;
            object lockObj = new object();

            for (var i = 0; i < numberOfAthletes; i++)
            {
                await semaphore.WaitAsync();
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        lock (trainers)
                        {
                            while (currentTrainerIndex < trainers.Count && trainers[currentTrainerIndex].FreePlaces == 0)
                            {
                                currentTrainerIndex++;
                            }

                            if (currentTrainerIndex < trainers.Count)
                            {
                                trainers[currentTrainerIndex].OccupyPlace();
                                lock (lockObj)
                                {
                                    soldMemberships++;
                                }
                            }
                            else
                            {
                                allAthletesAccommodated = false;
                            }
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine($"Total sold memberships: {soldMemberships}");
            return allAthletesAccommodated;
        }
    }
}
