using LabMooGame.MooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MooGame.Models
{
    public class MooFileDetails : IFileDetails
    {
        public string _filePath;
        public MooFileDetails()
        {
            _filePath = "hellotestresult.txt";
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
