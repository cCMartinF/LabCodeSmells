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
    GameLogic? gameLogic;

    #region constructor
    public IIO Io { get; }
    public IDAO Idao { get; }
    public NumberGuesser(IIO iO, IDAO iDao)
    {
        Io = iO;
        Idao = iDao;
    }
    #endregion

    public bool Run()
    {
       
        int targetValue, amountOfGuesses;
        GameInitialization(out gameLogic, out targetValue, out amountOfGuesses);
        Player player = SetUpPlayerData();

        Io.WriteOutput("Enter your guess: ");
        string guessedTargetValue = Io.ReadInput() ?? "";

        while (IsGameCompleted(targetValue, guessedTargetValue))
        {
            amountOfGuesses++;
            guessedTargetValue = Io.ReadInput() ?? "";
        }
        GameCompleted(amountOfGuesses, player);
        ContinuePlaying();

        return continuePlaying;
    }

    private void WriteResultOfComparedValues(int targetValue, int guessedValueAsNumber)
    {
            string message = guessedValueAsNumber switch
            {
                < 0 or > 100 => "Your guess is out of range!",
                _ => guessedValueAsNumber < targetValue ? "Too low! Try again."
                : guessedValueAsNumber > targetValue ? "Too high! Try again."
                : ""
            };
            Io.WriteOutput(message);
    }
    private void ContinuePlaying()
    {
        Io.WriteOutput("Do you want to play again? Y/y");
        string userSelection = Io.ReadInput().ToLower();
        if (userSelection == "y")
            continuePlaying = true;
    }

    private bool IsGameCompleted(int targetValue, string guessedValue)
    {
        int guessedValueAsNumber;
        if (int.TryParse(guessedValue, out guessedValueAsNumber))
        {
            WriteResultOfComparedValues(targetValue, guessedValueAsNumber);
            if (guessedValueAsNumber == targetValue)
                return false;
        }
        else
        {
            Io.WriteOutput("Invalid input.");
        }
        return true;
    }

    private void GameCompleted(int amountOfGuesses, Player player)
    {
        player.UpdateTotalAmountOfGuesses(amountOfGuesses);
        Io.WriteOutput(gameLogic.GetGameCompleteMessageWithGuesses(amountOfGuesses));
        Idao.UpdateGame(GameName, player);
        string highScore = gameLogic.GetHighScoresOfPlayersAverageGuesses(Idao.GetAllPlayers());
        Io.WriteOutput(highScore);
    }
    private Player SetUpPlayerData()
    {
        Io.WriteOutput("Please enter your name.");
        string playerName = Io.ReadInput() ?? "";
        Player player = Idao.GetPlayerData(playerName, GameName);
        return player;
    }

    private void GameInitialization(out GameLogic gameLogic, out int targetValue, out int amountOfGuesses)
    {
        gameLogic = new GameLogic();
        List<Player> playerList = new List<Player>();
        targetValue = Convert.ToInt32(gameLogic.CreateTargetValue(targetValueMaxLength));
        amountOfGuesses = 1;
    }
}
