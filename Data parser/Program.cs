

using System.Text.Json;

Console.WriteLine("----- Data parser -----");


var fileName = Console.ReadLine();

var fileContents = File.ReadAllText(fileName);

var videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);

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

Console.WriteLine("Pleae press any key to exit....");
Console.ReadKey();


public class VideoGame
{
    public string Title { get; init; }

    public int ReleaseYear { get; init; }

    public decimal Rating { get; init; }

    public override string ToString() => $"Title: {Title}, Release Year: {ReleaseYear}, Rating: {Rating}";

}   