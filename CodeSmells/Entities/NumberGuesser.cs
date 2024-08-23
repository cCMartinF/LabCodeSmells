using CodeSmells.Interfaces;
using CodeSmells.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Entities;

public class NumberGuesser : IGame
{
    public string GameName => "NumberGuesser";
    bool continuePlaying = true;
    int targetValueMaxLength = 2;

    #region constructor
    public IIO Io { get; }
    public IDAO Idao { get; }
    public NumberGuesser(IIO iO, IDAO iDao)
    {
        Io = iO;
        Idao = iDao;
    }
    #endregion

    public void Run()
    {
        GameLogic gameLogic = new GameLogic();
        List<Player> playerList = new List<Player>();
        int targetValue = Convert.ToInt32(gameLogic.CreateTargetValue(targetValueMaxLength));
        int amountOfGuesses = 0;
        int verifiedGuess;

        Io.WriteOutput("Please enter your name.");
        string playerName = Io.ReadInput() ?? "";
        Player player = Idao.GetPlayerDataFromFile(playerName, GameName);
        Io.WriteOutput("Enter your guess: ");

        while (continuePlaying)
        {
            string unverifiedGuess = Io.ReadInput();

            if (int.TryParse(unverifiedGuess, out verifiedGuess))
            {
                amountOfGuesses++;

                string message = verifiedGuess switch
                {
                    < 0 or > 100 => "Your guess is out of range!",
                    _ => verifiedGuess < targetValue ? "Too low! Try again."
                     : verifiedGuess > targetValue ? "Too high! Try again."
                     : gameLogic.GetGameCompleteMessageWithGuesses(amountOfGuesses)
                };

                Io.WriteOutput(message);

                if (verifiedGuess == targetValue)
                    break;

            }
        }
        player.UpdateTotalAmountOfGuesses(amountOfGuesses);
        Idao.UpdateGameFile(GameName, player);
        string highScore = gameLogic.GetHighScoresOfPlayersAverageGuesses(Idao.GetAllPlayers());
        Io.WriteOutput(highScore);
    }
}
