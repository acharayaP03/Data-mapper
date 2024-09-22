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
