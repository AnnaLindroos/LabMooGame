using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Interfaces;

public interface IGame
{
    void StartNewGame(string userName);
    void PlayRound();
    string GetUserGuess();
    string GenerateHint(string guess);
    bool IsCorrectGuess(string hint);
    bool UserWantsToContinue();
}
