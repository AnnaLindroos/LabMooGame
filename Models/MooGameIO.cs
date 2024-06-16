using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LabMooGame.Controllers;
using LabMooGame.Interfaces;

namespace LabMooGame.Models;

public class MooGameIO
{
    private IGame _game;

    private IIO _userIO;

    private IFileDetails _fileDetails;

    private bool _playGame;

    public MooGameIO(IIO userIO, IGoalGenerator goalGenerator, IFileDetails fileDetails)
    {
        _game = new MooGameController(userIO, goalGenerator, fileDetails);
        _userIO = userIO;
        _fileDetails = fileDetails;
        _playGame = true;
    }
}
