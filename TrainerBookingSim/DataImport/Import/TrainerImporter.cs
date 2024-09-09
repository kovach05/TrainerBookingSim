using System.Xml.Linq;
using BusinessLogic;
using DataAccess.Context;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataImport.Import;

public class TrainerImporter
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TrainerImporter> _logger;

    public TrainerImporter(ApplicationDbContext context, ILogger<TrainerImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ImportAsync(string filePath, string fileType)
    {
        List<Trainer> trainers = fileType.ToLower() switch
        {
            "json" => await LoadFromJsonFileAsync(filePath),
            "xml" => LoadFromXmlFile(filePath),
            _ => throw new ArgumentException("Unsupported file type.")
        };

        foreach (var trainer in trainers)
        {
            var entity = new Trainer
            {
                Name = trainer.Name,
                Price = trainer.Price,
                MaxTicket = trainer.MaxTicket,
                Popular = trainer.Popular,
                OccupiedPlaces = trainer.OccupiedPlaces,
                ExternalTrainerId = trainer.ExternalTrainerId
            };
            
            _context.Trainers.Add(entity);
        }
        
        await _context.SaveChangesAsync();
    }

    private async Task<List<Trainer>> LoadFromJsonFileAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var json = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<List<Trainer>>(json);
    }

    private List<Trainer> LoadFromXmlFile(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);

        return doc.Descendants("Trainer")
            .Select(trainer => new Trainer
            {
                Name = (string)trainer.Element("Name"),
                Price = (int)trainer.Element("Price"),
                MaxTicket = (int)trainer.Element("MaxTicket"),
                Popular = (int)trainer.Element("Popular"),
                OccupiedPlaces = (int)trainer.Element("OccupiedPlaces"),
                ExternalTrainerId = (string)trainer.Element("ExternalTrainerId")
            }).ToList();
    }
}
