using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMooGame.MooGame.Interfaces;

namespace LabMooGame.MooGame.Models;

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
}
