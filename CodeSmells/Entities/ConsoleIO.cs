using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Entities;

public class ConsoleIO : IIO
{
    public string ReadInput()
    {
        return Console.ReadLine() ?? "";
    }

    public void WriteOutput(string input)
    {
        Console.WriteLine(input);
    }
}
