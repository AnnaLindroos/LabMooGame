using LabMooGame.Interfaces;
using LabMooGame.MasterMind;
using LabMooGame.MooGame;

namespace LabMooGame.Models;

public class Controller : IController
{
    private const int MAX = 4;
    private IIO _userIO;
    private IGameLogic _gameLogic;
    private IHighScore _highScore;
    private IDataContext _dataContext;
    private string _winningSequence;
    private int _numberOfGuesses;
    private bool _isMooGame;
    private bool _playGame;

    public Controller(IIO userIO, IDataContext dataContext)
    {
        ArgumentNullException.ThrowIfNull(userIO);
        ArgumentNullException.ThrowIfNull(dataContext);
        _userIO = userIO;
        _dataContext = dataContext;
    }

    public bool RunProgram()
    {
        _playGame = true;
        string userName = PromptForUserName();

        while (_playGame)
        {
            if (UserWantsToPlay())
            {
                StartNewGame();
                PlayRound();
                _dataContext.CreateFile(userName, _numberOfGuesses, _isMooGame);
                _highScore = new HighScore(_dataContext);
                _highScore.GetPlayerResults(_isMooGame);
                _userIO.WriteLine("Player   games average");
                List<Player> highScores = _highScore.GetHighScoreBoard();
                foreach (Player player in highScores)
                {
                    _userIO.WriteLine($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
                }

                _userIO.WriteLine($"Correct, it took {_numberOfGuesses} guesses\nContinue?");

                if (!UserWantsToContinue())
                {
                    _playGame = false;
                }
            }
        }
        return false;
    }

    public bool UserWantsToPlay()
    {
        _userIO.WriteLine("Welcome! Please choose which game you want to play: \n1. MooGame \n2. MasterMind");
        string answer = _userIO.Read();

        switch (answer)
        {
            case "1":
                _isMooGame = true;
                _gameLogic = new MooGameLogic(MAX);
                return true;

            case "2":
                _isMooGame = false;
                _gameLogic = new MasterMindGameLogic(MAX);
                return true;

            case "q":
                _playGame = false;
                return false;

            default:
                _userIO.WriteLine("Please choose one of the games or press q to quit\n");
                return false;
        }
    }

    public string PromptForUserName()
    {
        _userIO.WriteLine("Enter your user name:\n");
        return _userIO.Read();
    }

    public void StartNewGame()
    {
        _numberOfGuesses = 0;
        _winningSequence = _gameLogic.GenerateWinningSequence();

        _userIO.WriteLine("New game:\n");
        _userIO.WriteLine($"For practice, number is: {_winningSequence} \n");
    }

    // Catches exceptions specific to the game round logic, such as errors in processing guesses.
    public void PlayRound()
    {
        while (true)
        {
            try
            {
                string userGuess = GetUserGuess();
                _numberOfGuesses++;
                string hint = _gameLogic.GenerateHint(userGuess);

                _userIO.WriteLine(hint + "\n");
                if (_gameLogic.IsCorrectGuess(hint))
                {
                    break;
                }
            }
            catch (Exception e)
            {
                _userIO.WriteLine($"Error processing your guess: {e.Message}\n");
            }
        }
    }

    public string GetUserGuess()
    {
        _userIO.WriteLine("Enter your guess:\n");
        return _userIO.Read();
    }

    public bool UserWantsToContinue()
    {
        try
        {
            string response = _userIO.Read();
            return string.IsNullOrWhiteSpace(response) || response[0].ToString().ToLower() != "n";
        }
        catch (Exception e)
        {
            _userIO.WriteLine($"Error reading your response: {e.Message}\n");
            return false;
        }
    }
}