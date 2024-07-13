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
    private IGoalGenerator _goalGenerator;
    private IHighScore _mooGameHighScore;
    private IFileDetails _mooFileDetails;
    private string _correctAnswer;
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
            _mooGameHighScore.UpdateHighScoreBoard();
            _mooGameHighScore.DisplayHighScoreBoard();

            _userIO.Write($"Correct, it took {_numberOfGuesses} guesses\nContinue?");

            if (!UserWantsToContinue())
            {
                playGame = false;
            }
        }
    }

    public void MakeGameResultsFile(string userName)
    {

        //StreamWriter output = new StreamWriter("mooresult.txt", append: true);
        StreamWriter output = new StreamWriter(_mooFileDetails.GetFilePath(), append: true);
        output.WriteLine($"{userName}#&#{_numberOfGuesses}");
        output.Close();
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

    public string GenerateHint(string userGuess)
    {
        int bulls = 0, cows = 0;
        userGuess = userGuess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_correctAnswer[i] == userGuess[j])
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
}


