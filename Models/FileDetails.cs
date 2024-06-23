using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models
{
    public class FileDetails : IFileDetails
    {
        public string _filePath = "testresult.txt";
        public FileDetails() 
        {
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
