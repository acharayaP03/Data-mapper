
namespace Data_parser.App;

public class DateParserApp
{
    private readonly IUserInteractor _userInteractor;
    private readonly IFilePrinter _filePrinter;
    private readonly IFileReader _fileReader;
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
