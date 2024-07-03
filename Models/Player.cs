using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;

public class Player
{
    public string PlayerName { get; private set; }

    public int NumberOfGames { get; private set; }

    public int GuessesInTotal { get; private set; }

    public Player(string playerName, int guesses)
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
        return PlayerName.Equals(((Player)player).PlayerName);
    }

    // WHEN USED?
    public override int GetHashCode()
    {
        return PlayerName.GetHashCode();
    }
}
