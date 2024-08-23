using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Interfaces;

public interface IIO
{
    public string ReadInput();
    public void WriteOutput(string input);
}
