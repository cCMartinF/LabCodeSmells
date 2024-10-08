﻿using System;
using System.IO;
using System.Collections.Generic;
using CodeSmells.Entities;

namespace MooGame
{
    class MainClass
	{
        public static void Main(string[] args)
		{

			bool continuePlaying = true;
			Console.WriteLine("Enter your user name:\n");
			string playerName = Console.ReadLine();

			while (continuePlaying)
			{
                int amountOfGuesses = 1;
                string targetValue = CreateTargetValue();

				Console.WriteLine($"New game:\n For practice, number is: {targetValue}\n");

				string guessedTargetValue = Console.ReadLine() ?? "";
				
				string comparedValue = CompareTargetAndGuess(targetValue, guessedTargetValue);

				Console.WriteLine(comparedValue + "\n");
				while (comparedValue != "BBBB,")
				{
					amountOfGuesses++;
					guessedTargetValue = Console.ReadLine() ?? "  ";
					Console.WriteLine($"{guessedTargetValue}\n");
					comparedValue = CompareTargetAndGuess(targetValue, guessedTargetValue);
					Console.WriteLine($"{comparedValue}\n");
				}
				StreamWriter logToDB = new StreamWriter("result.txt", append: true);
				logToDB.WriteLine(playerName + "#&#" + amountOfGuesses);
				logToDB.Close();
				GetTopList();
				Console.WriteLine("Correct, it took " + amountOfGuesses + " guesses\nContinue?");
				string answer = Console.ReadLine() ?? "";
				if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
				{
					continuePlaying = false;
				}
			}
		}
		static string CreateTargetValue()
		{
			Random randomGenerator = new Random();
			string goal = "";
			for (int i = 0; i < 4; i++)
			{
				int random = randomGenerator.Next(10);
				string randomDigit = "" + random;
				while (goal.Contains(randomDigit))
				{
					random = randomGenerator.Next(10);
					randomDigit = "" + random;
				}
				goal = goal + randomDigit;
			}
			return goal;
		}

		static string CompareTargetAndGuess(string goal, string guess)
		{
			int cows = 0, bulls = 0;
			guess += "    ";     // if player entered less than 4 chars
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (goal[i] == guess[j])
					{
						if (i == j)
						{
							bulls++;
						}
						else
						{
							cows++;
						}
					}
				}
			}
			return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
		}


		static void GetTopList()
		{
			StreamReader input = new StreamReader("result.txt");
			List<PlayerData> results = new List<PlayerData>();
			string line;
			while ((line = input.ReadLine()) != null)
			{
				string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
				string name = nameAndScore[0];
				int guesses = Convert.ToInt32(nameAndScore[1]);
				PlayerData pd = new PlayerData(name, guesses);
				int pos = results.IndexOf(pd);
				if (pos < 0)
				{
					results.Add(pd);
				}
				else
				{
					results[pos].Update(guesses);
				}
				
				
			}
			results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
			Console.WriteLine("Player   games average");
			foreach (PlayerData p in results)
			{
				Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
			}
			input.Close();
		}
	}
}