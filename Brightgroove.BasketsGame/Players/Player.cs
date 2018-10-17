using System;
using System.Collections.Generic;
using System.Threading;

namespace Brightgroove.BasketsGame.Players
{
    public abstract class Player
    {
        private static readonly Dictionary<PlayerType, Func<string, Player>> _players = new Dictionary<PlayerType, Func<string, Player>>
        {
            { PlayerType.Memory, name => new MemoryPlayer(name) },
            { PlayerType.Random, name => new RandomPlayer(name) },
            { PlayerType.Thorough, name => new ThoroughPlayer(name) },
            { PlayerType.CheaterRandom, name => new Cheater(new RandomPlayer(name)) },
            { PlayerType.CheaterThorough, name => new Cheater(new ThoroughPlayer(name)) }
        };

        public string Name { get; }

        public int LastGuess { get; set; }

        public int ClosestGuess { get; set; }

        public static Player CreatePlayer(string name, PlayerType playerType)
        {
            if(_players.ContainsKey(playerType))
            {
                return _players[playerType](name);
            }

            throw new ArgumentException("Player type is not supported");
        }

        protected Player(string name)
        {
            Name = name;
        }

        public abstract int GuessAlgorithm(Game game);

        public void GuessNumber(Game game)
        {
            LastGuess = GuessAlgorithm(game);
            game.MakeAttempt(this);
        }

        public void PutOnHold(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}