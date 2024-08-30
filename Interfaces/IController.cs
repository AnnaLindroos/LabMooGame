namespace LabMooGame.Interfaces;

public interface IController
{
    bool RunProgram();
    string PromptForUserName();
    bool UserWantsToPlay();
    void StartNewGame();
    void PlayRound();
    string GetUserGuess();
    bool UserWantsToContinue();
}
