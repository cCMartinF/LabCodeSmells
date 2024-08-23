using CodeSmells.Entities;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.GameFactory;

public class MasterMindFactory : IGameAbstractFactory
{
    public IGame CreateGame(IIO iO, IDAO dAo) => new MasterMind(iO, dAo);
}
