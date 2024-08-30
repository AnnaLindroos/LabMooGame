namespace LabMooGame.Interfaces;

public interface IPlayer
{
    public string PlayerName { get; set; }
    public int NumberOfGames { get; set; }
    public int GuessesInTotal { get; set; }
    void UpdatePlayerHighScore(int guesses);
    double GetAverageGuesses();
}
