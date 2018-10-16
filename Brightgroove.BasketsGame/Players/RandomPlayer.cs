using System;

namespace Brightgroove.BasketsGame.Players
{
    public class RandomPlayer : Player
    {
        private readonly Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        public RandomPlayer(string name) : base(name) { }

        public override int GuessAlgorithm(Game game)
        {
            return _rnd.Next(Constants.MinBasketWeight, Constants.MaxBasketWeight);
        }
    }
}