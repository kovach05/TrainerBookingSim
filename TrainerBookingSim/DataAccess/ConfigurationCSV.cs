using System.Globalization;
using BusinessLogic;
using BusinessLogic.Interface;
using CsvHelper;
using ILogger = BusinessLogic.Interface.ILogger;

public class ConfigurationCSV : ILoader<Trainer>
{
    private readonly ILogger _logger;
    public ConfigurationCSV(ILogger logger)
    {
        _logger = logger;
    }
    public List<Trainer> Load(string filePath)
    {
        _logger.LogInfo($"Start reading data: {filePath}");
            
        var trainers = new List<Trainer>();

        if (!File.Exists(filePath))
        {
            _logger.LogError($"File not found: {filePath}");
            throw new FileNotFoundException("File not found", filePath);
        }

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
        return csv.GetRecords<Trainer>().ToList();
    }
}