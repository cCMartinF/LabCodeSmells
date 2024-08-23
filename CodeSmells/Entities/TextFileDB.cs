using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Transactions;

namespace CodeSmells.Entities;

public class TextFileDB : IDAO
{
    StreamWriter _writeTextFile;
    StreamReader _readTextFile;
    private string _filePath, _fileLine;
    private int playerNameFromFile = 0, playerTotalGuessesFromFile = 1, playerTotalGamesFromFile = 2;

    public TextFileDB()
    {

    }
    public Player GetPlayerDataFromFile(string playerName, string gameName)
    {
       

        _filePath = $"{gameName}Result.txt";
        if (!DoesFilepathExist(_filePath))
                CreateNewGameFile(_filePath);

        while ((_fileLine = _readTextFile.ReadLine()) != null)
        {
            string[] playerFileAsStringArray = _fileLine.Split(new string[] { "#&#" }, StringSplitOptions.None);
            if (playerName == playerFileAsStringArray[playerNameFromFile])
            {
                int totalGuesses = Convert.ToInt32(playerFileAsStringArray[playerTotalGuessesFromFile]);
                int totalGamesPlayed = Convert.ToInt32(playerFileAsStringArray[playerTotalGamesFromFile]);
                Player existingPlayer = new Player(playerName, totalGuesses, totalGamesPlayed);
                _readTextFile.Close();
                return existingPlayer;
            }
        }
        _readTextFile.Close();
        Player newPlayer = new Player(playerName, 0, 0);
        return newPlayer;
    }
    public List<Player> GetAllPlayers ()
    {
        _readTextFile = new StreamReader(_filePath);
        List<Player> listOfAllPlayers = new List<Player>();
        while ((_fileLine = _readTextFile.ReadLine()) != null)
        {
            string[] playerFileAsStringArray = _fileLine.Split(new string[] { "#&#" }, StringSplitOptions.None);
            
                string playerName = playerFileAsStringArray[playerNameFromFile];
                int totalGuesses = Convert.ToInt32(playerFileAsStringArray[playerTotalGuessesFromFile]);
                int totalGamesPlayed = Convert.ToInt32(playerFileAsStringArray[playerTotalGamesFromFile]);
                Player existingPlayer = new Player(playerName, totalGuesses, totalGamesPlayed);
                listOfAllPlayers.Add(existingPlayer);
        }
        _readTextFile.Close();
        return listOfAllPlayers;
    }

    public void CreateNewGameFile(string filePath)
    {
        StreamWriter _writeTextFile = new StreamWriter(filePath);
        _writeTextFile.Close();
        _readTextFile = new StreamReader(filePath);

    }
    public void UpdateGameFile(string gameName, Player player)
    {
        _readTextFile = new StreamReader(_filePath);
        List<string> AllPlayerDatas = new List<string>();
        string playerData;

        while ((playerData = _readTextFile.ReadLine()) != null)
        {
            if (!playerData.StartsWith(player.Name))
            {
                AllPlayerDatas.Add(playerData);
            }
        }
        AllPlayerDatas.Add($"{player.Name}#&#{player.TotalGuess}#&#{player.NGames}");

        _readTextFile.Close();
        OverWriteGameFileWithNewData(AllPlayerDatas);
    }

    private void OverWriteGameFileWithNewData(List<string> lines)
    {
        _writeTextFile = new StreamWriter(_filePath, append:false);
        {
            foreach (var line in lines)
            {
                _writeTextFile.WriteLine(line);
            }
        }
        _writeTextFile.Close();
    }
    private bool DoesFilepathExist(string filePath)
    {
        try
        {
            _readTextFile = new StreamReader(filePath);
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        return true;
    }
}
