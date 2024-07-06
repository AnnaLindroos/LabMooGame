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
class MainClass
{
    const int MAX = 4;

    // moving out into a variable to make less vulnerable for changes

    // naming
    // funktioner/metoder
    // KOMMENTARER
    // klasser
    // FELHANTERING
    // CLEAN TESTS

    public static void Main(string[] args)
    {
        IGoalGenerator goalGenerator = new GoalGenerator(4);
        IIO uiIO = new ConsoleIO();
        IFileDetails fileDetails = new MooFileDetails();
        // LOGIK FÖR ATT VÄLJA ATT SPELA MOO ELLER MASTERMIND 
        IHighScore mooGameHighScore = new MooGameHighScore();
        MooGameController mooGameController = new(uiIO, goalGenerator, mooGameHighScore);
        mooGameController.PlayMooGame();
    }
}

