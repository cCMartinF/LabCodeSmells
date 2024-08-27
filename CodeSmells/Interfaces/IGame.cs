using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IGame
{
    public abstract string GameName { get; }
    public abstract IIO Io { get; }
    public abstract IDAO Idao { get; }
    public bool Run();

}
