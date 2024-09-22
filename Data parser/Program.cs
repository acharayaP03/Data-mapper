

using Data_parser.App;

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
