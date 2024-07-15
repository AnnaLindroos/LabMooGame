using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MooGame.Interfaces;

public interface IIO
{
    string Read();
    void Write(string message);
}
