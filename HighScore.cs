using LabMooGame.Interfaces;
using LabMooGame.MooGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame;

public class HighScore : IHighScore
{
    private IIO _userIO;
    private List<IPlayer> _results;
    private IFileDetails _fileDetails;

    public HighScore(IFileDetails fileDetails)
    {
        _userIO = new ConsoleIO();
        _fileDetails = fileDetails;
    }

    public void GetPlayerResults()
    {
        try
        {
            _results = ReadPlayerDataFromFile();
        }
        catch (Exception e)
        {
            _userIO.Write($"Error retrieving player results from file: {e.Message}");
        }
    }

    private List<IPlayer> ReadPlayerDataFromFile()
    {
        List<IPlayer> results = new List<IPlayer>();
        try
        {
            using (StreamReader input = new StreamReader(_fileDetails.GetFilePath()))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    ProcessPlayerData(line, results);
                }
            }
        }
        catch (Exception e)
        {
            _userIO.Write($"Error reading file: {e.Message}");
        }
        return results;
    }


    public void ProcessPlayerData(string line, List<IPlayer> results)
    {
        try
        {
            string[] playerNameAndScore = SplitPlayerNameAndScore(line);
            string playerName = playerNameAndScore[0];
            int guesses = Convert.ToInt32(playerNameAndScore[1]);

            UpdateOrAddPlayerResult(results, playerName, guesses);
        }
        catch (Exception e) when (e is FormatException || e is IndexOutOfRangeException)
        {
            _userIO.Write($"Error processing line '{line}': {e.Message}");
        }
    }

    private void UpdateOrAddPlayerResult(List<MooGamePlayer> results, string playerName, int guesses)
    {
        MooGamePlayer playerData = new MooGamePlayer(playerName, guesses);
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

    private string[] SplitPlayerNameAndScore(string line)
    {
        return line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    }


    // Kollar om spelarens data redan finns i listan. Om inte så läggs datan till, annars uppdateras spelarens befintliga highscore. 


    public void DisplayHighScoreBoard()
    {
        try
        {
            SortHighScoreResults();

            _userIO.Write("Player   games average");

            foreach (IPlayer player in _results)
            {
                _userIO.Write($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
            }
        }
        catch (Exception e)
        {
            _userIO.Write($"Error displaying high score board: {e.Message}");
        }
    }

    public void SortHighScoreResults()
    {
        _results.Sort((p1, p2) => p1.GetAverageGuesses().CompareTo(p2.GetAverageGuesses()));
    }
}
