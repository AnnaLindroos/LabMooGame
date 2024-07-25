using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Interfaces;

public interface IIO
{
    string Read();
    void Write(string message);
    void WriteLine(string message);
}
