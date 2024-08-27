using CodeSmells.Entities;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Mock;

public class MockDAO : IDAO
{
    public void CreateNewGameFile(string gameName)
    {
        throw new NotImplementedException();
    }

    public List<Player> GetAllPlayers()
    {
        List<Player> list = new List<Player>();
        list.Add(new Player("Kalle", 5, 1));
        return list;
    }

    public Player GetPlayerData(string playerName, string gameName)
    {
        throw new NotImplementedException();
    }

    public void UpdateGame(string gameName, Player player)
    {
        throw new NotImplementedException();
    }
}
