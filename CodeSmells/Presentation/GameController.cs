using CodeSmells.Entities;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Presentation;

public class GameController
{
    private readonly IIO _iO;
    private readonly IDAO _dAO;
    private readonly GameCreator _gameCreator;
    private readonly List<IGame> _games = new List<IGame>();
    private bool continuePlaying = true;

    public GameController(IIO iO, GameCreator gameCreator, IDAO dAO)
    {
        _iO = iO;
        _gameCreator = gameCreator;
        _dAO = dAO;
    }

    public void RunMainMenu()
    {
        Type[] gameList = GetGameListAsArray();
        string menuMessage = GetMenuMessageForGameSelection(gameList);

        while (true)
        {
            _iO.WriteOutput(menuMessage);
            string playerSelection = _iO.ReadInput().ToLower();
            IGame _game = _gameCreator.CreateGame(playerSelection, _iO, _dAO);
            PlayGame(_game);
        }
    }

    private string GetMenuMessageForGameSelection(Type[] gameList)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Please select you game.");
        for (int i = 0; i < gameList.Count() - 2; i++)
        {
            sb.Append(i + 1);
            sb.Append(" - ");
            sb.Append($"{gameList[i + 1].Name}\n");
        }
        sb.AppendLine("To quit, please press q.");

        return sb.ToString();
        
    }

    private Type[] GetGameListAsArray()
    {
        var type = typeof(IGame);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p)).ToArray();
        return types;
    }

    private void PlayGame(IGame _game)
    {
        
        while (continuePlaying)
        {
           continuePlaying = false;
            try
            {
                continuePlaying = _game.Run();
            }
            catch (ArgumentNullException)
            {
                _iO.WriteOutput("Error in starting game. Please try again.");
            }
        } 
        
    }
}
