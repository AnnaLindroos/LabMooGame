using LabMooGame.Interfaces;

namespace LabMooGame.UI;

public class ConsoleIO : IIO
{
    public ConsoleIO() { }

    public string Read()
    {
        return Console.ReadLine() ?? "";
    }

    public void Write(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine($"{message}\n");
    }
}
