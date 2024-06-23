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
    private IFileDetails _fileDetails;
    private IGoalGenerator _goalGenerator;
    private IHighScore _mooGameHighScore;

    private string _correctAnswer;
    private bool _playGame;
    private int _numberOfGuesses;

    //OBS STREAMWRITERN ANVÄNDS SAMTIDIFGRT AV MOOGAMEHIGHSCORE OCH CONTROLLERN; HUR DELA PÅ DESSA? 

    //Dependency injection ist för hårt kopplat 
    public MooGameController(IIO userIO, IGoalGenerator goalGenerator, IFileDetails fileDetails, IHighScore mooGameHighScore)
    {
        _userIO = userIO;
        _fileDetails = fileDetails;
        _goalGenerator = goalGenerator;
        _correctAnswer = string.Empty;
        _playGame = true;
        _mooGameHighScore = mooGameHighScore;
    }

    public int numberOfGuesses = 0;

    public void PlayMooGame()
    {
        _userIO.Write("Enter your user name:\n");
        string userName = _userIO.Read();

        while (_playGame)
        {

            StartNewGame(userName);
            PlayRound();
            _mooGameHighScore.GetHighScoreBoard();

            _userIO.Write($"Correct, it took {numberOfGuesses} guesses\nContinue?");

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

        string filePath = _fileDetails.GetFilePath();
        using StreamWriter output = new StreamWriter(filePath, append: true);
        output.WriteLine($"{userName}#&#{_numberOfGuesses}");
        output.Close();
    }

    public void PlayRound()
    {
        while (true)
        {
            string userGuess = GetUserGuess();
            string hint = GenerateHint(userGuess);

            _userIO.Write(hint + "\n");
            if (IsCorrectGuess(hint))
            {
                break;
            }

            _numberOfGuesses++;
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
        guess = guess.PadRight(MAXCharacters); // Ensure guess has at least 4 characters

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

}




/*numberOfGuesses = 1;

            string filePath = _fileDetails.GetFilePath();

            string correctAnswer = _goalGenerator.GenerateWinningSequence();

            MooGameHighScore highScore = new();

            

            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + correctAnswer + "\n");

            HintBullsAndCows();

            StreamWriter output = new StreamWriter(filePath, append: true);
            output.WriteLine(userName + "#&#" + numberOfGuesses);
            output.Close();

            highScore.GetHighScoreBoard();

            

            string keepPlaying = _userIO.Read();
            if (keepPlaying != null && keepPlaying != "" && keepPlaying.Substring(0, 1) == "n")
            {
                _playGame = false;
            }
        }
    }
  
s
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
} */


