using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MasterMind.Models;

public class MasterMindGoalGenerator : IGoalGenerator
{
    private int _goalLength;

    public MasterMindGoalGenerator(int goalLength)
    {
        _goalLength = goalLength;
    }
    public string GenerateWinningSequence()
    {
        Random randomNumbers = new Random();

        string correctAnswer = string.Empty;

        for (int i = 0; i < _goalLength; i++)
        {
            int randomNumber = randomNumbers.Next(6);

            string generatedNumberSequence = randomNumber.ToString();
            correctAnswer += generatedNumberSequence;
        }
        return correctAnswer;
    }
}

