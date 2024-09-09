using System.Xml.Linq;
using Common.Models;
using DataAccess.Context;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataImport.Import;

public class ClientImporter
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ClientImporter> _logger;

    public ClientImporter(ApplicationDbContext context, ILogger<ClientImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ImportAsync(string filePath, string fileType)
    {
        List<Client> clients = fileType.ToLower() switch
        {
            "json" => await LoadFromJsonFileAsync(filePath),
            "xml" => LoadFromXmlFile(filePath),
            _ => throw new ArgumentException("Unsupported file type.")
        };

        foreach (var client in clients)
        {
            var entity = new Client
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                ExternalClientId = client.ExternalClientId
            };

            _context.Clients.Add(entity);
        }

        await _context.SaveChangesAsync();
    }


    private async Task<List<Client>> LoadFromJsonFileAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var json = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<List<Client>>(json);
    }

    private List<Client> LoadFromXmlFile(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);

        return doc.Descendants("Client")
            .Select(client => new Client
            {
                FirstName = (string)client.Element("FirstName"),
                LastName = (string)client.Element("LastName"),
                ExternalClientId = (string)client.Element("ExternalClientId")
            }).ToList();
    }
}
