using System.Xml.Serialization;
using BusinessLogic;
using BusinessLogic.Interface;

namespace DataAccess;

public class ConfigurationXML : ILoader<Trainer>
{
    private readonly ILogger _logger;
        
    public ConfigurationXML(ILogger logger)
    {
        _logger = logger;
    }
        
    public List<Trainer> Load(string filePath)
    {
        var trainers = new List<Trainer>();

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Trainer>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                trainers = (List<Trainer>)serializer.Deserialize(fileStream)!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error reading XML file: {ex.Message}");
        }

        return trainers;
    }
}