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

        public void CloseFile(StreamReader input)
        {
            input.Close();
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
