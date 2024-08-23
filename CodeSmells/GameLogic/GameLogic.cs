using CodeSmells.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Logic;

public class GameLogic
{
    public string CreateTargetValue(int howLargeTargetValue)
    {
        Random randomNumberGenerator = new Random();
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < howLargeTargetValue; i++)
        {
            stringBuilder.Append(randomNumberGenerator.Next(10));
        }
        return stringBuilder.ToString() ?? "Error in creating target value.";
    }

    public string GetGameCompleteMessageWithGuesses(int guesses)
    {
        return $"Correct, it took {guesses}  guesses \n";
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

}
