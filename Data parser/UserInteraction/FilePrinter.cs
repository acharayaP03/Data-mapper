using Data_parser.Model;

public class FilePrinter : IFilePrinter
{

    private readonly IUserInteractor _userInteractor;

    public FilePrinter(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public void Print(List<DataModel> dataModel)
    {
        if (dataModel.Count > 0)
        {
            _userInteractor.PrintMessage(Environment.NewLine + "Loaded file read data are here: ");
            _userInteractor.PrintMessage("Loaded games are: :");
            foreach (var game in dataModel)
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
