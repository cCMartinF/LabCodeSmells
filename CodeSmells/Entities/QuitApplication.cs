using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Entities;

public class QuitApplication : IGame
{
    public string GameName => throw new NotImplementedException();

    public IIO Io => throw new NotImplementedException();

    public IDAO Idao => throw new NotImplementedException();

    public void Quit()
    {
        throw new NotImplementedException();
    }

    public void Run()
    {
        Environment.Exit(0);
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}
