using LabMooGame.Models;

namespace LabMooGame.Interfaces;

public interface IDataContext
{
    void CreateFile(string userName, int numberOfGuesses, bool isMooGame);
    string GetFilePath(bool isMooGame);
    List<Player> ReadPlayerDataFromFile(bool isMooGame);
    void CheckIfPlayerExists(List<Player> players, string playerName, int guesses);
}
