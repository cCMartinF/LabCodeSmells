using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSmells.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSmells.Interfaces;
using CodeSmells.GameFactory;
using CodeSmells.Mock;

namespace CodeSmells.Entities.Tests
{
   

    [TestClass()]
    public class GameCreatorTests
    {
        private IIO iO;
        private IDAO dAO;
        private IGameAbstractFactory _gameAbstractFactory;

        [TestInitialize]
        public void Initialize()
        {
            iO = new ConsoleIO();
            dAO = new MockDAO();
        }
        

        [TestMethod()]
        public void Create_MasterMindGameFromAbstractFactory_ReturnMasterMind()
        {
            _gameAbstractFactory = new MasterMindFactory();
            var expected = new MasterMind(iO, dAO);


            var actual = _gameAbstractFactory.CreateGame(iO, dAO);

            Assert.AreEqual(actual.GameName, expected.GameName);
        }
        [TestMethod()]
        public void Create_NumberGuesserGameFromAbstractFactory_ReturnNumberGuesser()
        {
            _gameAbstractFactory = new NumberGuesserFactory();
            var expected = new NumberGuesser(iO, dAO);


            var actual = _gameAbstractFactory.CreateGame(iO, dAO);

            Assert.AreEqual(actual.GameName, expected.GameName);
        }
    }
}