using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Interfaces;

public interface IFileDetails
{
    void CloseFile(StreamReader input);

    string GetFilePath();
}
