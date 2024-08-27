using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeSmells.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSmells.Interfaces;
using CodeSmells.Entities;
using CodeSmells.Mock;
using static System.Formats.Asn1.AsnWriter;

namespace CodeSmells.Logic.Tests
{
    [TestClass()]
    public class GameLogicTests
    {
        IRand _mockRand;
        GameLogic gameLogic;
        IDAO _dAO;

        [TestInitialize()]
        public void Init() 
        {
            _mockRand = new MockRand();
            gameLogic = new GameLogic(_mockRand);
            _dAO = new MockDAO();
        }
        
        [TestMethod()]
        public void CreateTargetValueTest_ReturnEmptyIfZeroLength()
        {
            var result = gameLogic.CreateTargetValue(0);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod()]
        public void CreateTargetValueTest_LengthIsCorrect()
        {
            var result = gameLogic.CreateTargetValue(3);

            var expected = 3;

            Assert.AreEqual(result.Length, expected);
        }

        [TestMethod()]
        public void GetGameMessageWithGuessesTest_ReturnStringWithAmountGuesses()
        {
            int guesses = 1;
            var result = gameLogic.GetGameCompleteMessageWithGuesses(guesses);

            var expected = $"Correct, it took 1 guesses \n";

            Assert.AreEqual(expected, result);

        }

        [TestMethod()]
        public void GetHighScoresOfPlayersAverageGuessesTest_WithMockedPlayerDataAndZeroGuesses_ReturnHighScore()
        {
            var mockPlayers = _dAO.GetAllPlayers();
            var result = gameLogic.GetHighScoresOfPlayersAverageGuesses(mockPlayers);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("High Score");
            for (int i = 0; i < mockPlayers.Count(); i++)
            {
                sb.Append(i + 1);
                sb.Append(" - ");
                sb.Append($"{mockPlayers[i].Name}");
                sb.Append(" - ");
                sb.Append($"{mockPlayers[i].GetAverageScore()} guess per game.\n");
            }
            sb.AppendLine("---------");

            var expected = sb.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetRightAndWrongTest_WithZeroInput_ReturnZeroRight()
        {
            string targetValue = "1234", guessedValue = "";
            var result = gameLogic.GetRightAndWrong(targetValue, guessedValue);

            var expected = 0;
            Assert.AreEqual(expected, result.Item1);
        }

        [TestMethod()]
        public void GetRightAndWrongTest_WithHalfInput_ReturnTwoRight()
        {
            string targetValue = "1234", guessedValue = "12";
            var result = gameLogic.GetRightAndWrong(targetValue, guessedValue);

            var expected = 2;
            Assert.AreEqual(expected, result.Item1);
        }

        [TestMethod()]
        public void GetRightAndWrongTest_WithHalfInput_ReturnTwoWrong()
        {
            string targetValue = "1234", guessedValue = "34";
            var result = gameLogic.GetRightAndWrong(targetValue, guessedValue);

            var expected = 2;
            Assert.AreEqual(expected, result.Item2);
        }
        [TestMethod()]
        public void GetRightAndWrongTest_WithFullInput_ReturnThreeWrongAndOneRight()
        {
            string targetValue = "1234", guessedValue = "4132";
            var result = gameLogic.GetRightAndWrong(targetValue, guessedValue);

            var expectedRight = 1;
            var expectedWrong = 3;
            Assert.AreEqual(expectedWrong, result.Item2);
            Assert.AreEqual(expectedRight, result.Item1);
        }
    }
}