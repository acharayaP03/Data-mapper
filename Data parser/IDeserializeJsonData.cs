using Data_parser.Model;

public interface IDeserializeJsonData
{
    List<VideoGame> Deserialize(string? fileName, string fileContents);
}