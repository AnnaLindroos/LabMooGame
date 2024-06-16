using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LabMooGame.Interfaces;
using LabMooGame.Models;

namespace LabMooGame.Controllers;

// static?? 
public class MooGameController : IGame
{
    private string _correctAnswer;

    private IIO _userIO;

    private bool _playGame;

    private IFileDetails _fileDetails;

    private IGoalGenerator _goalGenerator;

    //Dependency injection ist för hårt kopplat 
    public MooGameController(IIO uiIO, IGoalGenerator goalGenerator, IFileDetails fileDetails)
    {
        _correctAnswer = string.Empty;
        _userIO = uiIO;
        _playGame = true;
        _fileDetails = fileDetails;
        _goalGenerator = goalGenerator;
    }

    const int MAX = 4;

    public int numberOfGuesses = 0;

    public void PlayMooGame()
    {
        _userIO.Write("Enter your user name:\n");

        string userName = _userIO.Read();

        while (_playGame)
        {
            numberOfGuesses = 1;

            string filePath = _fileDetails.GetFilePath();

            string correctAnswer = _goalGenerator.GenerateWinningSequence();

            MooGameHighScore highScore = new();

            _userIO.Write("New game:\n");

            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + correctAnswer + "\n");

            HintBullsAndCows();

            StreamWriter output = new StreamWriter(filePath, append: true);
            output.WriteLine(userName + "#&#" + numberOfGuesses);
            output.Close();

            highScore.GetHighScoreBoard();

            _userIO.Write($"Correct, it took {numberOfGuesses} guesses\nContinue?");

            string keepPlaying = _userIO.Read();
            if (keepPlaying != null && keepPlaying != "" && keepPlaying.Substring(0, 1) == "n")
            {
                _playGame = false;
            }
        }
    }
  

    public void HintBullsAndCows()
    {
        string userGuess = _userIO.Read();
        string bullsAndCows = CheckUserGuess(userGuess);
        _userIO.Write(bullsAndCows + "\n");
        while (bullsAndCows != "BBBB,")
        {
            numberOfGuesses++;
            userGuess = _userIO.Read();
            _userIO.Write(userGuess + "\n");
            bullsAndCows = CheckUserGuess(userGuess);
            _userIO.Write(bullsAndCows + "\n");
        }
    }


    //handle error input
    public string CheckUserGuess(string guess)
    {
        int cows = 0, bulls = 0;
        //OBS felhantering här???? Är det del av uppgiften eller inte? Ändrar funktionalitet. FÅR lägga till try/catch
        //if (guess.Length <4 || )
        guess += "    ";     // if player entered less than 4 chars
        for (int answerIndex = 0; answerIndex < MAX; answerIndex++)
        {
            for (int guessIndex = 0; guessIndex < MAX; guessIndex++)
            {
                if (_correctAnswer[answerIndex] == guess[guessIndex])
                {
                    if (answerIndex == guessIndex)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }

        string hintBulls = new('B', bulls);
        string hintCows = new('C', cows);
        StringBuilder hintResult = new();
        return hintResult.Append(hintBulls).Append(',').Append(hintCows).ToString();
    }
}

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

public interface IGoalGenerator 
{
    string GenerateWinningSequence();
}
