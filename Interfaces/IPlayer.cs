using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Interfaces;

public interface IPlayer
{
    string PlayerName { get; set; }
    int NumberOfGames { get; set; }
    int GuessesInTotal { get; set; }
    void UpdatePlayerHighScore(int guesses);
    double GetAverageGuesses();
    bool Equals(object player);
    int GetHashCode();
}
