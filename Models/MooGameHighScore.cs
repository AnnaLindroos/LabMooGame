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
    private IIO _userIO;
    private List<Player> _results;
    private IFileDetails _mooFileDetails;

    public MooGameHighScore(IFileDetails mooFileDetails)
    {
        _userIO = new ConsoleIO();
        _mooFileDetails = mooFileDetails;
    }

    public void UpdateHighScoreBoard()
    {
        //StreamReader input = new StreamReader("mooresult.txt");
        StreamReader input = new StreamReader(_mooFileDetails.GetFilePath());
        List<Player> results = new List<Player>();
        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string playerName = playerNameAndScore[0];
            int guesses = Convert.ToInt32(playerNameAndScore[1]);

            Player playerData = new Player(playerName, guesses);
            int pos = results.IndexOf(playerData);
            if (pos < 0)
            {
                results.Add(playerData);
            }
            else
            {
                results[pos].UpdatePlayerHighScore(guesses);
            }
        }
        _results = results;
        input.Close();
    }

    public void DisplayHighScoreBoard()
    {
        SortHighScoreResults();

        _userIO.Write("Player   games average");

        foreach (Player player in _results)
        {
            _userIO.Write($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
        }
    }

    public void SortHighScoreResults()
    {
        _results.Sort((p1, p2) => p1.GetAverageGuesses().CompareTo(p2.GetAverageGuesses()));
    }
}
