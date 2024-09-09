using BusinessLogic;
using BusinessLogic.Interface;
using Newtonsoft.Json;
using ILogger = BusinessLogic.Interface.ILogger;
using JsonException = System.Text.Json.JsonException;

namespace DataAccess;

public class ConfigurationJSON : ILoader<Trainer>
{
    private readonly ILogger _logger;

    public ConfigurationJSON(ILogger logger)
    {
        _logger = logger;
    }

    public List<Trainer> Load(string filePath)
    {
        _logger.LogInfo($"Start reading data: {filePath}");

        var items = new List<Trainer>();

        if (!File.Exists(filePath))
        {
            _logger.LogError($"File not found: {filePath}");
            throw new FileNotFoundException("File not found", filePath);
        }

        try
        {
            var jsonData = File.ReadAllText(filePath);
            items = JsonConvert.DeserializeObject<List<Trainer>>(jsonData);
            _logger.LogInfo("Data deserialized successfully");
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"JSON Error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error reading JSON file: {ex.Message}");
            throw;
        }

        return items;
    }
}