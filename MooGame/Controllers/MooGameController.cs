using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LabMooGame.MooGame.Models;
using LabMooGame.MooGame.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public void PlayGame()
    {
        _userIO.Write("Enter your user name:\n");
        string userName = _userIO.Read();
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
            catch (Exception ex)
            {
                _userIO.Write($"Error processing your guess: {ex.Message}\n");
            }
        }
    }

    // Captures exceptions when reading user input to ensure a smooth experience even if input fails.
    public string GetUserGuess()
    {
        try
        {
            _userIO.Write("Enter your guess:\n");
            return _userIO.Read();
        }
        catch (Exception ex)
        {
            _userIO.Write($"Error reading your guess: {ex.Message}\n");
            return string.Empty;
        }
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

    // Handles exceptions related to file operations, such as file access issues.
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

    // Catches errors when reading user responses, ensuring that even if an error occurs, the game can continue gracefully.
    public bool UserWantsToContinue()
    {
        try
        {
            string response = _userIO.Read();
            return string.IsNullOrWhiteSpace(response) || response[0].ToString().ToLower() != "n";
        }
        catch (Exception ex)
        {
            _userIO.Write($"Error reading your response: {ex.Message}\n");
            return false;
        }
    }
}


