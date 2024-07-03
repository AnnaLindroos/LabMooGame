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
    private List<Player> _results;
    private IIO _userIO;

    public MooGameHighScore()
    {
        _results = new List<Player>();
        _userIO = new ConsoleIO();
    }

    public void GetHighScoreBoard()
    {
        SortHighScoreResults();
        DisplayHighScoreBoard();
    }

    public void UpdateHighScoreBoard()
    {
        using (StreamReader input = new StreamReader("mooresult.txt"))
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                Player pd = new Player(name, guesses);
                int pos = _results.IndexOf(pd);
                if (pos < 0)
                {
                    _results.Add(pd);
                }
                else
                {
                    _results[pos].UpdatePlayerHighScore(guesses);
                }

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

        foreach (Player player in _results)
        {
            _userIO.Write($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
        }
    }
}
