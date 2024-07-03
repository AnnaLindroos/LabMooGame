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
    private const int MAXCharacters = 4;
    private IIO _userIO;
    private IFileDetails _mooFileDetails;
    private IGoalGenerator _goalGenerator;
    private IHighScore _mooGameHighScore;
    private string _correctAnswer;
    private bool _playGame;
    private int _numberOfGuesses;

    public MooGameController(IIO userIO, IGoalGenerator goalGenerator, IFileDetails mooFileDetails)
    {
        _userIO = userIO;
        _mooFileDetails = mooFileDetails;
        _goalGenerator = goalGenerator;
        _mooGameHighScore = new MooGameHighScore();
        _playGame = true;
    }

    public void PlayMooGame()
    {
        _userIO.Write("Enter your user name:\n");
        string userName = _userIO.Read();

        while (_playGame)
        {
            _numberOfGuesses = 0;
            StartNewGame(userName);
            PlayRound();
            UpdateResults(userName);
            _mooGameHighScore.GetHighScoreBoard();

            _userIO.Write($"Correct, it took {_numberOfGuesses} guesses\nContinue?");

            if (!UserWantsToContinue())
            {
                _playGame = false;
            }
        }
    }

    public void StartNewGame(string userName)
    {
        _numberOfGuesses = 0;
        _correctAnswer = _goalGenerator.GenerateWinningSequence();

        _userIO.Write("New game:\n");
        _userIO.Write("For practice, number is: " + _correctAnswer + "\n");
    }

    public void PlayRound()
    {
        while (true)
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
    }

    public string GetUserGuess()
    {
        _userIO.Write("Enter your guess:\n");
        return _userIO.Read();
    }

    public string GenerateHint(string guess)
    {
        int bulls = 0, cows = 0;
        guess = guess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_correctAnswer[i] == guess[j])
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

    public bool UserWantsToContinue()
    {
        string response = _userIO.Read();
        return string.IsNullOrWhiteSpace(response) || response[0].ToString().ToLower() != "n";
    }

    private void UpdateResults(string userName)
    {
        using (StreamWriter output = new StreamWriter("mooresult.txt", append: true))
        {
            output.WriteLine($"{userName}#&#{_numberOfGuesses}");
        }
        _mooGameHighScore.UpdateHighScoreBoard();
    }
}


