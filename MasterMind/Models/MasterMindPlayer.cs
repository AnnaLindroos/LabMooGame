using LabMooGame.Interfaces;
using LabMooGame.MooGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MasterMind.Models;

public class MasterMindPlayer : IPlayer
{
    public string PlayerName { get; private set; }

    public int NumberOfGames { get; private set; }

    public int GuessesInTotal { get; private set; }

    public MasterMindPlayer(string playerName, int guesses)
    {
        PlayerName = playerName;
        NumberOfGames = 1;
        GuessesInTotal = guesses;
    }

    public void UpdatePlayerHighScore(int guesses)
    {
        GuessesInTotal += guesses;
        NumberOfGames++;
    }

    public double GetAverageGuesses()
    {
        return (double)GuessesInTotal / NumberOfGames;
    }

    //BRO WHEN ARE THESE USED
    public override bool Equals(object player)
    {
        return PlayerName.Equals(((MasterMindPlayer)player).PlayerName);
    }

    // WHEN USED?
    public override int GetHashCode()
    {
        return PlayerName.GetHashCode();
    }
}