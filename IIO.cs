using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame;

public interface IIO
{
    public void Read();
    public void Write(string message);
}
