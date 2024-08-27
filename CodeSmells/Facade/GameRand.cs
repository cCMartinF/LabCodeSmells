using CodeSmells.Interfaces;

namespace CodeSmells.Facade;

public class GameRand : IRand
{
    public int Next(int maxValue)
    {
        Random randomNumberGenerator = new Random();
        return randomNumberGenerator.Next(maxValue);
    }
}
