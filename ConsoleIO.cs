using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMooGame.Interfaces;

namespace LabMooGame;

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
