using LabMooGame.MooGame.Interfaces;
using MooGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MooGame.Models;
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

    //Calls the ReadPlayerDataFromFile method to get the list of player results.
    public void UpdateHighScoreBoard()
    {
        List<Player> results = ReadPlayerDataFromFile();
        _results = results;
    }

    // Reads lines from the file and processes each line.
    //Uses a using statement for StreamReader to ensure the file is closed automatically.
    private List<Player> ReadPlayerDataFromFile()
    {
        List<Player> results = new List<Player>();
        using (StreamReader input = new StreamReader(_mooFileDetails.GetFilePath()))
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                ProcessLine(line, results);
            }
        }
        return results;
    }

    // Processes each line, splitting the line into player name and score, and updating the results list accordingly.
    private void ProcessLine(string line, List<Player> results)
    {
        string[] playerNameAndScore = ParsePlayerData(line);
        string playerName = playerNameAndScore[0];
        int guesses = Convert.ToInt32(playerNameAndScore[1]);

        UpdateResultsList(results, playerName, guesses);
    }

    // Splits a line into an array containing the player's name and score.
    private string[] ParsePlayerData(string line)
    {
        return line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    }


    // Checks if the player data is already in the list. If not, adds it; otherwise, updates the player's high score.
    private void UpdateResultsList(List<Player> results, string playerName, int guesses)
    {
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

    /*public void UpdateHighScoreBoard()
    {
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
    } */

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
