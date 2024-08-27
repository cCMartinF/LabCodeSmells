using CodeSmells.Interfaces;
using CodeSmells.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Entities;

public class MasterMind : IGame
{

    public string GameName => "MasterMind";
    
    int targetValueMaxLength = 4;
    GameLogic gameLogic;
    bool continuePlaying = false;


    #region constructor
    public IIO Io { get; }
    public IDAO Idao {  get; }

    public MasterMind(IIO iO, IDAO iDao)
    {
        Io = iO;
        Idao = iDao;
        gameLogic = new GameLogic();
    }
    #endregion
    public bool Run()
    {

        string targetValue;
        int amountOfGuesses;
       

        GameInitialization(out gameLogic, out targetValue, out amountOfGuesses);
        Player player = SetUpPlayerData();

        Io.WriteOutput($"New game:\n For practice, number is: {targetValue}\n");
        string guessedTargetValue = Io.ReadInput() ?? "";


        while (IsGameCompleted(targetValue, guessedTargetValue))
        {
            WriteCowsAndBulls(gameLogic.GetRightAndWrong(targetValue, guessedTargetValue));
            amountOfGuesses++;
            guessedTargetValue = Io.ReadInput() ?? "";
        }
        GameCompleted(amountOfGuesses, player);
        ContinuePlaying();

        return continuePlaying;

    }

    private void ContinuePlaying()
    {
        Io.WriteOutput("Do you want to play again? Y/y");
        string userSelection = Io.ReadInput().ToLower();
        if (userSelection == "y")
            continuePlaying = true;
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

    private bool IsGameCompleted(string targetValue, string guessedTargetValue)
    {
        (int rightGuesses, int wrongGuesses) result = gameLogic.GetRightAndWrong(targetValue, guessedTargetValue);

        if (result.rightGuesses == targetValueMaxLength)
            return false;
        return true;
    }

    private void GameInitialization(out GameLogic gameLogic, out string targetValue, out int amountOfGuesses)
    {
        gameLogic = new GameLogic();
        List<Player> playerList = new List<Player>();
        targetValue = gameLogic.CreateTargetValue(targetValueMaxLength);
        amountOfGuesses = 1;
    }
    private void WriteCowsAndBulls((int right, int wrong) rightAndWrong)
    {
        string result = new string('B', rightAndWrong.right) + ',' + new string('C', rightAndWrong.wrong);
        Io.WriteOutput(result);
    }
}
