using LabMooGame.Interfaces;
using System.Text;

namespace LabMooGame.MasterMind;

public class MasterMindGameLogic : IGameLogic
{
    private int _goalLength;
    private const int MAX = 4;
    private string _winningSequenceShuffled;
    private string _winningSequenceString = "000011112222333344445555";

    public MasterMindGameLogic(int goalLength)
    {
        if (goalLength is <= 0 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(goalLength), goalLength, "input must be between 1 and 10");
        }
        _goalLength = goalLength;
    }
    public string GenerateWinningSequence()
    {
        Random randomNumbers = new Random();
        var shuffled = _winningSequenceString.OrderBy(x => randomNumbers.Next());
        var substring = shuffled.Take(_goalLength);
        _winningSequenceShuffled = new string(substring.ToArray());
        return _winningSequenceShuffled;
    }

    public string GenerateHint(string userGuess)
    {
        userGuess = userGuess.PadRight(MAX);
        StringBuilder hint = new();

        for (int i = 0; i < MAX; i++)
        {
            if (userGuess[i] == _winningSequenceShuffled[i])
            {
                hint.Append('X');
            }
            else
            {
                hint.Append('-');
            }
        }
        return hint.ToString();
    }

    public bool IsCorrectGuess(string hint)
    {
        return hint == "XXXX";
    }
}

