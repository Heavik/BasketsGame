using System.Collections.Generic;

namespace Brightgroove.BasketsGame.Players
{
    public class MemoryPlayer : RandomPlayer
    {
        private readonly HashSet<int> _alreadyGuessed = new HashSet<int>();

        public MemoryPlayer(string name) : base(name)
        {
        }

        public override int GuessAlgorithm(Game game)
        {
            int guess = base.GuessAlgorithm(game);
            while(_alreadyGuessed.Contains(guess))
            {
                guess = base.GuessAlgorithm(game);
            }
            _alreadyGuessed.Add(guess);

            return guess;
        }
    }
}
