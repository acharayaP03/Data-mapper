public class LocalFileReader : IFileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
}
