using CodeSmells.Entities;
using CodeSmells.Facade;
using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Logic;

public class GameLogic
{
    IRand _rand;
    public GameLogic(IRand rand)
    {
        _rand = rand;
    }
    public GameLogic()
    {
        _rand = new GameRand();
    }
    //TODO UnitTest varför vänta

    public string CreateTargetValue(int howLargeTargetValue)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < howLargeTargetValue; i++)
        {
            stringBuilder.Append(_rand.Next(10));
        }
        return stringBuilder.ToString() ?? "Error in creating target value.";
    }

    public string GetGameCompleteMessageWithGuesses(int guesses)
    {
        return $"Correct, it took {guesses} guesses \n";
    }


    public string GetHighScoresOfPlayersAverageGuesses(List<Player> players)
    {
            List<Player> sortedListOfPlayers = new List<Player>();
            sortedListOfPlayers = GetSortPlayerBasedOnAverage(players);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("High Score");
            for (int i = 0; i < sortedListOfPlayers.Count(); i++)
            {
                sb.Append(i + 1);
                sb.Append(" - ");
                sb.Append($"{sortedListOfPlayers[i].Name}");
                sb.Append(" - ");
            sb.Append($"{sortedListOfPlayers[i].GetAverageScore()} guess per game.\n");
            }
            sb.AppendLine("---------");

            return sb.ToString();

    }
    private List<Player> GetSortPlayerBasedOnAverage(List<Player> players)
    {
        var sortedPlayers = players.OrderBy(p => p.GetAverageScore()).ToList();
        return sortedPlayers;
    }

    public (int, int) GetRightAndWrong(string targetGoal, string userGuess)
    {
        int wrong = 0, right = 0;
        bool[] indexWithBull = new bool[targetGoal.Length];

        for (int i = 0; i < userGuess.Length; i++)
            if (targetGoal[i] == userGuess[i])
            {
                right++;
                indexWithBull[i] = true;
            }

        for (int i = 0; i < userGuess.Length; i++)
            if (!indexWithBull[i])
            {
                for (int j = 0; j < targetGoal.Length; j++)
                    if (targetGoal[j] == userGuess[i])
                        wrong++;
            }

        return (right, wrong);
    }

}
