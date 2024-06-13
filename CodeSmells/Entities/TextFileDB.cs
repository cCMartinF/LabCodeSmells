using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Transactions;

namespace CodeSmells.Entities;

public class TextFileDB : IDAO
{
    StreamReader _readTextFile;
    StreamWriter _writeTextFile;

    public TextFileDB()
    {
        
    }
    public void Create(string game, string playerName, string result)
    {
        _writeTextFile = new StreamWriter($"{game}.txt", append:true);  
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public void Get()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
