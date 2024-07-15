using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.MooGame.Interfaces;

public interface IHighScore
{
    void UpdateHighScoreBoard();
    void SortHighScoreResults();
    void DisplayHighScoreBoard();
}
