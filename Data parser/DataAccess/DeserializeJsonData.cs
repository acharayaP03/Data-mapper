using Data_parser.Model;
using System.Text.Json;

public class DeserializeJsonData : IDeserializeJsonData
{

    private readonly IUserInteractor _userInteractor;

    public DeserializeJsonData(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public List<DataModel> Deserialize(string? fileName, string fileContents)
    {


        try
        {
            return JsonSerializer.Deserialize<List<DataModel>>(fileContents);
        }
        catch (JsonException ex)
        {
            _userInteractor.PrintError($"JSON in {fileName} file was not in a vlid format. JSON body.");
            _userInteractor.PrintError(fileContents);

            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
    }
}