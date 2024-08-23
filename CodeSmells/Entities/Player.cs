namespace CodeSmells.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        public int TotalGuess {  get; private set; }
        public int NGames { get; private set; }


        public Player(string name, int totalGuesses, int nGames)
        {
            Name = name;
            NGames = nGames;
            TotalGuess = totalGuesses;
            
        }

        public void UpdateTotalAmountOfGuesses(int amountOfGuesses)
        {
            TotalGuess += amountOfGuesses;
            UpdateTotalAmountOfGames();
        }

        private void UpdateTotalAmountOfGames()
        {
            NGames++;
        }

        public double GetAverageScore()
        {
            return (double)TotalGuess/NGames;
        }
    }
}