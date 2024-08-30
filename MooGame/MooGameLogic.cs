using LabMooGame.Interfaces;

namespace LabMooGame.MooGame;
public class MooGameLogic : IGameLogic
{
    private int _goalLength;
    private const int MAXCharacters = 4;
    private string _winningSequenceShuffled;
    private string _winningSequenceString = "0123456789";
    public MooGameLogic(int goalLength)
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
        int bulls = 0, cows = 0;
        //Added input padding to ensure the guess has at least four characters.
        userGuess = userGuess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_winningSequenceShuffled[i] == userGuess[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }
        return new string('B', bulls) + "," + new string('C', cows);
    }

    public bool IsCorrectGuess(string hint)
    {
        return hint == "BBBB,";
    }
}
