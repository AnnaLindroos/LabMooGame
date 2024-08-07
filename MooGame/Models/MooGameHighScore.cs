﻿using LabMooGame.Interfaces;
using MooGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LabMooGame.MooGame.Models;
//static or no????
public class MooGameHighScore : IHighScore
{
    private IIO _userIO;
    private List<MooGamePlayer> _results;
    private IFileDetails _mooFileDetails;

    public MooGameHighScore(IFileDetails mooFileDetails)
    {
        _userIO = new ConsoleIO();
        _mooFileDetails = mooFileDetails;
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

    private List<MooGamePlayer> ReadPlayerDataFromFile()
    {
        List<MooGamePlayer> results = new List<MooGamePlayer>();
        try
        {
            using (StreamReader input = new StreamReader(_mooFileDetails.GetFilePath()))
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


    public void ProcessPlayerData(string line, List<MooGamePlayer> results)
    {
        try
        {
            string[] playerNameAndScore = SplitPlayerNameAndScore(line);
            string playerName = playerNameAndScore[0];
            int guesses = Convert.ToInt32(playerNameAndScore[1]);

            UpdatePlayerResults(results, playerName, guesses);
        }
        catch (Exception e) when (e is FormatException || e is IndexOutOfRangeException)
        {
            _userIO.Write($"Error processing line '{line}': {e.Message}");
        }
    }

    private string[] SplitPlayerNameAndScore(string line)
    {
        return line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    }


    // Kollar om spelarens data redan finns i listan. Om inte så läggs datan till, annars uppdateras spelarens befintliga highscore. 
    private void UpdatePlayerResults(List<MooGamePlayer> results, string playerName, int guesses)
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

    public void DisplayHighScoreBoard()
    {
        try
        {
            SortHighScoreResults();

            _userIO.Write("Player   games average");

            foreach (MooGamePlayer player in _results)
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
