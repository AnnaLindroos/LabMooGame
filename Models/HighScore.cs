using LabMooGame.Interfaces;

namespace LabMooGame.Models;

public class HighScore : IHighScore
{
    private List<Player> _players;
    private IDataContext _dataContext;

    public HighScore(IDataContext dataContext)
    {
        ArgumentNullException.ThrowIfNull(dataContext);
        _dataContext = dataContext;
    }

    public void GetPlayerResults(bool isMooGame)
    {
        try
        {
            _players = _dataContext.ReadPlayerDataFromFile(isMooGame);
        }
        catch (Exception e)
        {
            throw new($"Error retrieving player results from file: {e.Message}");
        }
    }

    public List<Player> GetHighScoreBoard()
    {
        try
        {
            SortHighScoreResults();
            return _players;
        }
        catch (Exception e)
        {
            throw new($"Error displaying high score board: {e.Message}");
        }
    }

    public void SortHighScoreResults()
    {
        _players.Sort((p1, p2) => p1.GetAverageGuesses().CompareTo(p2.GetAverageGuesses()));
    }
}
