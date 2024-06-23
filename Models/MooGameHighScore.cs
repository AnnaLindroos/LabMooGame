using LabMooGame.Interfaces;
using MooGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;
//static or no????
public class MooGameHighScore : IHighScore
{
    private string _filePath;
    private StreamReader _input;
    private List<PlayerData> _results;
    private IIO _userIO;
    private IFileDetails _fileDetails;

    public MooGameHighScore()
    {
        _fileDetails = new FileDetails();
        _input = new StreamReader(_fileDetails.GetFilePath());
        _results = new();
        _userIO = new ConsoleIO();
    }

    public void GetHighScoreBoard()
    {
        UpdateHighScoreBoard();
        SortHighScoreResults();
        DisplayHighScoreBoard();
        _input.Close();
    }

    public void UpdateHighScoreBoard()
    {
        string line;
        while ((line = _input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);

            PlayerData playerData = new PlayerData(name, guesses);
            int playerIndex = _results.IndexOf(playerData);

            if (playerIndex < 0)
            {
                _results.Add(playerData);
            }
            else
            {
                _results[playerIndex].UpdatePlayerHighScore(guesses);
            }
        }
    }

    public void SortHighScoreResults()
    {
        _results.Sort((p1, p2) => p1.GetAverageGuesses().CompareTo(p2.GetAverageGuesses()));
    }

    public void DisplayHighScoreBoard()
    {
        _userIO.Write("Player   games average");

        foreach (PlayerData player in _results)
        {
            ///// OBS CONSOLE WRITELINE HERE
            ///// Removed the D after "{1,5}" since number of games doesn't need to be in decimal format
            ///// Also usin string interpolation to increase readability. 
            ///KÄLLA: "String interpolation provides a more readable, convenient syntax to format strings. It's easier to read than string composite formatting. "
            ///https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated

            _userIO.Write($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
        }
    }
}
