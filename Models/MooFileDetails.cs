using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models
{
    public class MooFileDetails : IFileDetails
    {
        public string _filePath = "mooresult.txt";
        public MooFileDetails() 
        {
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
