using CodeSmells.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Transactions;

namespace CodeSmells.Entities;

public class TextFileDao : IDAO
{
    StreamWriter? _writeTextFile;
    StreamReader? _readTextFile;
    private string filePath;
    private int playerNameFromFile = 0, playerTotalGuessesFromFile = 1, playerTotalGamesFromFile = 2;

    public TextFileDao()
    {

    }

    //TODO Gör facade precis som i GameLogic for StreamReader / Writer så du inte låser upp dig
    public Player GetPlayerData(string playerName, string gameName)
    {
        filePath = $"{gameName}Result.txt";
        if (!DoesFilepathExist(filePath))
                CreateNewGameFile(filePath);

        return AllPlayersInGameFile().FirstOrDefault(n => n.Name == playerName) ?? new Player(playerName, 0, 0);
    }
    public List<Player> GetAllPlayers ()
    {
        return AllPlayersInGameFile();
    }

    private List<Player> AllPlayersInGameFile()
    {
        _readTextFile = new StreamReader(filePath);
        List<Player> listOfAllPlayers = new List<Player>();
        string fileData;

        while ((fileData = _readTextFile.ReadLine()) != null)
        {
            string[] playerFileAsStringArray = fileData.Split(new string[] { "#&#" }, StringSplitOptions.None);

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
        // _readTextFile = new StreamReader(filePath);

    }
    public void UpdateGame(string gameName, Player player)
    {
        _readTextFile = new StreamReader(filePath);
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
        _writeTextFile = new StreamWriter(filePath, append:false);
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
            _readTextFile .Close();
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        return true;
    }
}
