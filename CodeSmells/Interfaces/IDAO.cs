using CodeSmells.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IDAO
{
    void CreateNewGameFile(string gameName);
    Player GetPlayerDataFromFile(string playerName, string gameName);
    void UpdateGameFile(string gameName, Player player);

    List<Player> GetAllPlayers();
}
