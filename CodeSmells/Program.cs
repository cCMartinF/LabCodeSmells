using System;
using System.IO;
using System.Collections.Generic;
using CodeSmells.Entities;
using System.Text;
using CodeSmells.Interfaces;
using CodeSmells.Presentation;
using CodeSmells;

namespace MooGame
{
    class Program
	{
        public static void Main(string[] args)
		{
            IIO io = new ConsoleIO();
			IDAO dao = new TextFileDB();
            GameCreator gameCreator = new GameCreator();
            GameController gameController = new GameController(io, gameCreator, dao);
			gameController.RunMainMenu();
		}
	}
}