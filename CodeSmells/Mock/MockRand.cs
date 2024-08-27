using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Mock;

public class MockRand : IRand
{
    public int Next(int maxValue)
    {
        return 3;
    }
}
