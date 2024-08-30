using LabMooGame.Interfaces;

namespace LabMooGame.Models;

public class Player : IPlayer
{
    public string PlayerName { get; set; }
    public int NumberOfGames { get; set; }
    public int GuessesInTotal { get; set; }

    public Player(string playerName, int guesses)
    {
        ArgumentNullException.ThrowIfNull(playerName);
        ArgumentNullException.ThrowIfNull(guesses);
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
}
