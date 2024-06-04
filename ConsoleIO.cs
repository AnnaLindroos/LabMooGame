using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame;

public class ConsoleIO : IIO
{
    void IIO.Read()
    {
        Console.ReadLine();
    }

    void IIO.Write(string message)
    {
        throw new NotImplementedException();
    }
}
