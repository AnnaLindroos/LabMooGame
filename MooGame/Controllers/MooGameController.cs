using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LabMooGame.MooGame.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LabMooGame.Interfaces;

namespace LabMooGame.MooGame.Controllers;

public class MooGameController : IGame
{
    private const int MAXCharacters = 4;
    private IIO _userIO;
    private IGoalGenerator _goalGenerator;
    private IHighScore _mooGameHighScore;
    private IFileDetails _mooFileDetails;
    private string _winningSequence;
    private int _numberOfGuesses;

    public MooGameController(IIO userIO, IGoalGenerator goalGenerator, IHighScore mooGameHighScore, IFileDetails mooFileDetails)
    {
        _userIO = userIO;
        _goalGenerator = goalGenerator;
        _mooGameHighScore = mooGameHighScore;
        _mooFileDetails = mooFileDetails;
    }

    public void PlayGame(string userName)
    {
        bool playGame = true;

        while (playGame)
        {
            StartNewGame(userName);
            PlayRound();
            MakeGameResultsFile(userName);
            _mooGameHighScore.GetPlayerResults();
            _mooGameHighScore.DisplayHighScoreBoard();

            _userIO.Write($"Correct, it took {_numberOfGuesses} guesses\nContinue?");

            if (!UserWantsToContinue())
            {
                playGame = false;
            }
        }
    }

    public void StartNewGame(string userName)
    {
        _numberOfGuesses = 0;
        _winningSequence = _goalGenerator.GenerateWinningSequence();

        _userIO.Write("New game:\n");
        _userIO.Write("For practice, number is: " + _winningSequence + "\n");
    }

    // Catches exceptions specific to the game round logic, such as errors in processing guesses.
    public void PlayRound()
    {
        while (true)
        {
            try
            {
                string userGuess = GetUserGuess();
                _numberOfGuesses++;
                string hint = GenerateHint(userGuess);

                _userIO.Write(hint + "\n");
                if (IsCorrectGuess(hint))
                {
                    break;
                }
            }
            catch (Exception e)
            {
                _userIO.Write($"Error processing your guess: {e.Message}\n");
            }
        }
    }

    public string GetUserGuess()
    {

        _userIO.Write("Enter your guess:\n");
        return _userIO.Read();
  
    }

    public string GenerateHint(string userGuess)
    {
        int bulls = 0, cows = 0;
        userGuess = userGuess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_winningSequence[i] == userGuess[j])
                {
                    if (i == j)
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
        return new string('B', bulls) + "," + new string('C', cows);
    }

    public bool IsCorrectGuess(string hint)
    {
        return hint == "BBBB,";
    }

    public void MakeGameResultsFile(string userName)
    {
        try
        {
            using (StreamWriter output = new StreamWriter(_mooFileDetails.GetFilePath(), append: true))
            {
                output.WriteLine($"{userName}#&#{_numberOfGuesses}");
            }
        }
        catch (Exception e)
        {
            _userIO.Write($"Error writing to the results file: {e.Message}\n");
        }
    }

    public bool UserWantsToContinue()
    {
        try
        {
            string response = _userIO.Read();
            return string.IsNullOrWhiteSpace(response) || response[0].ToString().ToLower() != "n";
        }
        catch (Exception e)
        {
            _userIO.Write($"Error reading your response: {e.Message}\n");
            return false;
        }
    }
}


