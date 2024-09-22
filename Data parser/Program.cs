

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
    public void Run()
    {
        var isFileRead = false;
        var fileName = default(string);
        var fileContents = default(string);

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
                fileContents = File.ReadAllText(fileName);
                isFileRead = true;
            }


        } while (!isFileRead);

        List<VideoGame> videoGames = default;

        try
        {
            videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);
        }
        catch (JsonException ex)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"JSON in {fileName} file was not in a vlid format. JSON body.");
            Console.WriteLine(fileContents);
            Console.ForegroundColor = originalColor;


            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
        videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);

        if (videoGames.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Loaded games are: :");
            foreach (var game in videoGames)
            {
                Console.WriteLine(game);
                Console.WriteLine();
                //Console.WriteLine($"Title: {game.Title}, Release Year: {game.ReleaseYear}, Rating: {game.Rating}");
            }
        }
        else
        {
            Console.WriteLine("No games found in the file.");
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