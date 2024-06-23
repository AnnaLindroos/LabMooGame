﻿using System;
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
        IFileDetails fileDetails = new FileDetails();
        IHighScore mooGameHighScore = new MooGameHighScore();
        // LOGIK FÖR ATT VÄLJA ATT SPELA MOO ELLER MASTERMIND 
        MooGameController mooGameController = new(uiIO, goalGenerator, fileDetails, mooGameHighScore);
        mooGameController.PlayMooGame();
    }
}

