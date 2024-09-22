

using System.IO.Enumeration;
using System.Text.Json;

var userInteractor = new ConsoleUserInteractor();

var app = new DateParserApp(
    userInteractor,
    new FilePrinter(userInteractor),
    new DeserializeJsonData(userInteractor),
    new LocalFileReader()
    );

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
    private readonly IFilePrinter _filePrinter;
    private readonly IFileReader    _fileReader;
    private readonly IDeserializeJsonData _deserializeJsonData;

    public DateParserApp(IUserInteractor userInteractor, IFilePrinter filePrinter, IDeserializeJsonData deserializeJsonData, IFileReader fileReader)
    {
        _userInteractor = userInteractor;
        _filePrinter = filePrinter;
        _deserializeJsonData = deserializeJsonData;
        _fileReader = fileReader;
    }
    public void Run()
    {
        string? fileName = _userInteractor.ReadValidFilePath();

        var fileContents = _fileReader.Read(fileName);
        var videoGames = _deserializeJsonData.Deserialize(fileName, fileContents);
        _filePrinter.Print(videoGames);
    }

}

public class VideoGame
{
    public string Title { get; init; }

    public int ReleaseYear { get; init; }

    public decimal Rating { get; init; }

    public override string ToString() => $"Title: {Title}, Release Year: {ReleaseYear}, Rating: {Rating}";

}


public interface IFileReader
{
   string Read(string path);
}


public class LocalFileReader : IFileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
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

public class FilePrinter : IFilePrinter
{

    private readonly IUserInteractor _userInteractor;

    public FilePrinter(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public void Print(List<VideoGame> videoGames)
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
}

public class DeserializeJsonData : IDeserializeJsonData
{

    private readonly IUserInteractor _userInteractor;

    public DeserializeJsonData(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public List<VideoGame> Deserialize(string? fileName, string fileContents)
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