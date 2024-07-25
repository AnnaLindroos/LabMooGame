using LabMooGame.MooGame.Models;
using LabMooGame.MooGame.Controllers;
using LabMooGame;
using LabMooGame.Interfaces;
using System.Runtime.InteropServices;
using LabMooGame.MasterMind.Models;

namespace MooGame;

class Program
{
    public static void Main(string[] args)
    {
        const int MAX = 4;
        IIO userIO = new ConsoleIO();
        userIO.Write("Enter your user name:\n");
        string userName = userIO.Read();

        userIO.Write("Welcome! Please choose which game you want to play: \n1. MooGame \n2.MasterMind");

        string answer = userIO.Read().ToUpper();
        switch (answer)
        {
            case "1":
                PlayerChoseMooGame(userName);
                break;

            case "2":
                PlayerChoseMasterMind(userName);
                break;


            case "Q":
                break;

            default:
                userIO.Write("Please choose one of the games or press q to quit");
                break;
        }

        void PlayerChoseMooGame(string userName)
        {
            IGoalGenerator goalGenerator = new MooGameGoalGenerator(MAX);
            IFileDetails mooFileDetails = new MooGameFileDetails();
            IHighScore mooGameHighScore = new MooGameHighScore(mooFileDetails);
            MooGameController mooGameController = new(userIO, goalGenerator, mooGameHighScore, mooFileDetails);
            mooGameController.PlayGame(userName);
        }

        void PlayerChoseMasterMind(string userName)
        {
            IGoalGenerator goalGenerator = new MasterMindGoalGenerator(MAX);
            IFileDetails masterMindFileDetails = new MasterMindFileDetails();
            //IHighScore masterMindHighScore = new
        }
    }
}

