using Data_parser.Model;

public interface IDeserializeJsonData
{
    List<DataModel> Deserialize(string? fileName, string fileContents);
}