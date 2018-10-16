namespace Brightgroove.BasketsGame.Players
{
    public class Cheater : Player
    {
        private readonly Player _player;

        public Cheater(Player player) : base(player.Name)
        {
            _player = player;
        }

        public override int GuessAlgorithm(Game game)
        {
            int guess = _player.GuessAlgorithm(game);
            while(game.TriedNumbers.Contains(guess))
            {
                guess = _player.GuessAlgorithm(game);
            }

            return guess;
        }
    }
}