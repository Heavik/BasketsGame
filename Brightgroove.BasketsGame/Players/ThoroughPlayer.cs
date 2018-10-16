namespace Brightgroove.BasketsGame.Players
{
    public class ThoroughPlayer : Player
    {
        private int _nextGuess = Constants.MinBasketWeight;

        public ThoroughPlayer(string name) : base(name) {}

        public override int GuessAlgorithm(Game game)
        {
            _nextGuess++;
            return _nextGuess;
        }
    }
}