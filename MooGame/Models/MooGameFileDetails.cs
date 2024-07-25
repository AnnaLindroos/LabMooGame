using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MooGame.Models;

public class MooGameFileDetails : IFileDetails
{
    public string _filePath;
    public MooGameFileDetails()
    {
        _filePath = "hellomoogameresult.txt";
    }

    public string GetFilePath()
    {
        return _filePath;
    }
}
