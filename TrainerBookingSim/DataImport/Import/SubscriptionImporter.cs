using System.Xml.Linq;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataImport.Import;

public class SubscriptionImporter
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SubscriptionImporter> _logger;

    public SubscriptionImporter(ApplicationDbContext context, ILogger<SubscriptionImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ImportSubscriptionsAsync(string filePath, string fileType)
    {
        List<Subscription> subscriptions = fileType.ToLower() switch
        {
            "json" => await LoadFromJsonFileAsync(filePath),
            "xml" => LoadFromXmlFile(filePath),
            _ => throw new ArgumentException("Unsupported file type.")
        };

        foreach (var subscription in subscriptions)
        {
            var entity = new Subscription
            {
                UserId = subscription.UserId,
                ClientId = subscription.ClientId,
                Visits = subscription.Visits,
                CreatedDate = subscription.CreatedDate,
                ExpiryDate = subscription.ExpiryDate,
                IsExpired = subscription.IsExpired,
                TrainerId = subscription.TrainerId,
                ExternalSubscriptionId = subscription.ExternalSubscriptionId
            };

            _context.Subscriptions.Add(entity);
        }

        await _context.SaveChangesAsync();
    }


    private async Task<List<Subscription>> LoadFromJsonFileAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var json = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<List<Subscription>>(json);
    }

    private List<Subscription> LoadFromXmlFile(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);

        return doc.Descendants("Subscription")
            .Select(subscription => new Subscription
            {
                UserId = (string)subscription.Element("UserId"),
                Visits = (int)subscription.Element("Visits"),
                CreatedDate = (DateTime)subscription.Element("CreatedDate"),
                ExpiryDate = (DateTime)subscription.Element("ExpiryDate"),
                IsExpired = (bool)subscription.Element("IsExpired"),
                TrainerId = (string?)subscription.Element("TrainerId"),
                ExternalSubscriptionId = (int)subscription.Element("ExternalSubscriptionId")
            }).ToList();
    }
}
