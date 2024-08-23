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
    bool continuePlaying = true;
    int targetValueMaxLength = 4;

    #region constructor
    public IIO Io { get; }
    public IDAO Idao {  get; }

    public MasterMind(IIO iO, IDAO iDao)
    {
        Io = iO;
        Idao = iDao;
    }
    #endregion
    public void Run()
    {
        GameLogic gameLogic = new GameLogic();
        List<Player> playerList = new List<Player>();
        string targetValue = gameLogic.CreateTargetValue(targetValueMaxLength);
        int amountOfGuesses = 1;

        Io.WriteOutput("Please enter your name.");
        string playerName = Io.ReadInput() ?? "Null";
        Player player = Idao.GetPlayerDataFromFile(playerName, GameName);
        Io.WriteOutput($"New game:\n For practice, number is: {targetValue}\n");

        while (continuePlaying)
        {
            string guessedTargetValue = Io.ReadInput() ?? "";
            string cowsAndBulls = GetCowsAndBulls(targetValue, guessedTargetValue);
            Io.WriteOutput(cowsAndBulls + "\n");

            if (guessedTargetValue == targetValue)
            {
                break;
            }
            amountOfGuesses++;
        }

        player.UpdateTotalAmountOfGuesses(amountOfGuesses);
        Io.WriteOutput(gameLogic.GetGameCompleteMessageWithGuesses(amountOfGuesses));
        Idao.UpdateGameFile(GameName, player);
        string highScore = gameLogic.GetHighScoresOfPlayersAverageGuesses(Idao.GetAllPlayers());
        Io.WriteOutput(highScore);
    }

    private string GetCowsAndBulls(string goal, string guess)
    {
        int cows = 0, bulls = 0;
        bool[] indexWithBull = new bool[goal.Length];

        for (int i = 0; i < guess.Length; i++)
            if (goal[i] == guess[i])
            {
                bulls++;
                indexWithBull[i] = true;
            }

        for (int i = 0; i < guess.Length; i++)
            if (!indexWithBull[i])
            {
                for (int j = 0; j < goal.Length; j++)
                    if (goal[j] == guess[i])
                        cows++;
            }

        string result = new string('B', bulls) + "," + new string('C', cows);
        return result;
    }
}
