﻿using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;
public class GoalGenerator : IGoalGenerator
{
    private int _goalLength;

    public GoalGenerator(int goalLength)
    {
        _goalLength = goalLength;
    }
    public string GenerateWinningSequence()
    {
        Random randomNumbers = new Random();

        string correctAnswer = string.Empty;

        for (int i = 0; i < _goalLength; i++)
        {
            int randomNumber = randomNumbers.Next(10);

            string generatedNumberSequence = randomNumber.ToString();
            while (correctAnswer.Contains(generatedNumberSequence))
            {
                randomNumber = randomNumbers.Next(10);
                generatedNumberSequence = randomNumber.ToString();
            }
            correctAnswer += generatedNumberSequence;
        }
        return correctAnswer;
    }
}
