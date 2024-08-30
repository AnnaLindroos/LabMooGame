namespace LabMooGame.Interfaces;

public interface IGameLogic
{
    string GenerateWinningSequence();
    string GenerateHint(string userGuess);
    bool IsCorrectGuess(string hint);
}
