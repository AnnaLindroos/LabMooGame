using LabMooGame.Interfaces;
using LabMooGame.Models;
using LabMooGame.Data;
using LabMooGame.UI;

namespace MooGame;

class Program
{
    public static void Main(string[] args)
    {
        IIO userIO = new ConsoleIO();
        
        IDataContext dataContext = new DataContext(DataContextConfigCreator.CreateConfig());

        Controller controller = new Controller(userIO, dataContext);
        controller.RunProgram();
    }
}

