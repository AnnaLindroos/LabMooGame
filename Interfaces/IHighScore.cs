using LabMooGame.Models;

namespace LabMooGame.Interfaces;

public interface IHighScore
{
    void GetPlayerResults(bool isMooGame);
    void SortHighScoreResults();
    List<Player> GetHighScoreBoard();
}
