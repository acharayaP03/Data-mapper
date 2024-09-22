

using System.IO.Enumeration;
using System.Text.Json;

var app = new DateParserApp();
var logger = new Logger("applicationLog.txt");



try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Sorry! The application has experienced an unexpected error and will to be closed.");
    logger.Log(ex);
}


Console.WriteLine("Pleae press any key to exit....");
Console.ReadKey();

public class DateParserApp
{
    private readonly IUserInteractor _userInteractor;

    public DateParserApp(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }
    public void Run()
    {
        string? fileName = _userInteractor.ReadValidFilePath();

        var fileContents = File.ReadAllText(fileName);
        List<VideoGame> videoGames = DeserializeJsonDataFromFile(fileName, fileContents);

        videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);

        PrintDataFromJsonSerealized(videoGames);
    }

    private void PrintDataFromJsonSerealized(List<VideoGame> videoGames)
    {
        if (videoGames.Count > 0)
        {
            _userInteractor.PrintMessage(Environment.NewLine + "Loaded file read data are here: ");
            _userInteractor.PrintMessage("Loaded games are: :");
            foreach (var game in videoGames)
            {
                _userInteractor.PrintMessage(game.ToString());
            }
        }
        else
        {
            _userInteractor.PrintMessage("No games found in the file.");
        }
    }

    private  List<VideoGame> DeserializeJsonDataFromFile(string? fileName, string fileContents)
    {


        try
        {
            return JsonSerializer.Deserialize<List<VideoGame>>(fileContents);
        }
        catch (JsonException ex)
        {
            _userInteractor.PrintError($"JSON in {fileName} file was not in a vlid format. JSON body.");
            _userInteractor.PrintError(fileContents);

            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
    }
}


public class VideoGame
{
    public string Title { get; init; }

    public int ReleaseYear { get; init; }

    public decimal Rating { get; init; }

    public override string ToString() => $"Title: {Title}, Release Year: {ReleaseYear}, Rating: {Rating}";

}

public interface IUserInteractor
{
    string? ReadValidFilePath();

    void PrintMessage(string message);

    void PrintError(string message);
}

public class ConsoleUserInteractor : IUserInteractor
{

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintError(string message)
    {
        var originalColor = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
    public string ReadValidFilePath()
    {
        var isValidFilePath = false;
        var fileName = default(string);

        do
        {

            Console.WriteLine("----- File data  parser -----");
            fileName = Console.ReadLine();

            if (fileName is null)
            {
                Console.WriteLine($"the file name cannot be null");
            }
            else if (fileName == string.Empty)
            {
                Console.WriteLine($"the file name cannot be empty");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine($"the file name cannot be found");
            }
            else
            {
                isValidFilePath = true;
            }


        } while (!isValidFilePath);
        return fileName;
    }
}