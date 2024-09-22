
namespace Data_parser.Model
{
    public class VideoGame
    {
        public string Title { get; init; }

        public int ReleaseYear { get; init; }

        public decimal Rating { get; init; }

        public override string ToString() => $"Title: {Title}, Release Year: {ReleaseYear}, Rating: {Rating}";

    }
}
