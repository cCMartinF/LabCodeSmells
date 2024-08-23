using CodeSmells.GameFactory;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeSmells.Entities;

public class GameCreator
{
    private readonly Dictionary<string, Func<IIO, IDAO, IGame>> _gameCreator;
    IGameAbstractFactory _gameAbstractFactory;
    bool noGameSelected = true;
    public GameCreator()
    {

        _gameCreator = new Dictionary<string, Func<IIO, IDAO, IGame>>()
        {
            { "1",  (io, dao) => CreateMasterMindGame(io, dao) },
            { "2", (io, dao) => CrateNumberGuesserGame(io, dao) },
            { "q", (io, dao) => QuitApplication() }
        };
    }
    public IGame CreateGame(string userInput, IIO io, IDAO dAO)
    {
        while (noGameSelected)
        {
            if (_gameCreator.ContainsKey(userInput))
            {
                break;
            }
            io.WriteOutput("Invalid input, no games exist of the given index.");
            userInput = io.ReadInput().ToLower();
        }
        return _gameCreator[userInput](io, dAO);

    }
    public IGame CreateMasterMindGame(IIO iO, IDAO dAO)
    {
        _gameAbstractFactory = new MasterMindFactory();
        IGame game = _gameAbstractFactory.CreateGame(iO, dAO);
        return game;
    }

    public IGame CrateNumberGuesserGame(IIO iO, IDAO dAO)
    {
        _gameAbstractFactory = new NumberGuesserFactory();
        IGame game = _gameAbstractFactory.CreateGame(iO, dAO);
        return game;
    }

    //TODO Skapa en IGame som "löser" quitMetoden när den returneras till GameController

    public IGame QuitApplication()
    {
        IGame quitApplication = new QuitApplication();
        return quitApplication;
    }
}
