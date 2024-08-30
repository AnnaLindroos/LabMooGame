using LabMooGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameTests1;

[TestClass]
public class PlayerTests
{
    private string _playerName;
    private int _guesses;

    [TestInitialize]
    public void Setup()
    {
        _playerName = "Anna";
        _guesses = 5;
    }

    [TestMethod]
    public void PlayerContructorTest()
    {
        Player player = new Player(_playerName, _guesses);

        Assert.AreEqual(_playerName, player.PlayerName);
        Assert.AreEqual(1, player.NumberOfGames);
        Assert.AreEqual(_guesses, player.GuessesInTotal);
    }

    [TestMethod]
    public void UpdatePlayerHighScoreTest()
    {
        Player player = new Player(_playerName, _guesses);

        player.UpdatePlayerHighScore(3);

        Assert.AreEqual(2, player.NumberOfGames);
        Assert.AreEqual(8, player.GuessesInTotal);
    }

    [TestMethod]
    public void GetAverageGuessesTest()
    {
        Player player = new Player(_playerName, 6);
        player.UpdatePlayerHighScore(4);

        double averageGuesses = player.GetAverageGuesses();

        Assert.AreEqual(5.0, averageGuesses);
    }
}
