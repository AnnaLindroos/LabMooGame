using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MasterMind.Models;

internal class MasterMindFileDetails : IFileDetails
{
    public string _filePath;
    public MasterMindFileDetails()
    {
        _filePath = "hellomastermindresult.txt";
    }

    public string GetFilePath()
    {
        return _filePath;
    }
}
