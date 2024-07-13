using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using LabMooGame.Interfaces;
using LabMooGame.Models;
using LabMooGame.Controllers;

//Fokus namngivning och utbrytning

namespace MooGame;

//Remove Mainclass?
class Program
{
    public static void Main(string[] args)
    {
        const int MAX = 4;

        // LOGIK FÖR ATT VÄLJA ATT SPELA MOO ELLER MASTERMIND 
        IGoalGenerator goalGenerator = new GoalGenerator(MAX);
        IIO uiIO = new ConsoleIO();
        IFileDetails mooFileDetails = new MooFileDetails();
        IHighScore mooGameHighScore = new MooGameHighScore(mooFileDetails);
        MooGameController mooGameController = new(uiIO, goalGenerator, mooGameHighScore, mooFileDetails);
        mooGameController.PlayGame();
    }
}

