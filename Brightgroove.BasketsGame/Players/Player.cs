using System;
using System.Collections.Generic;
using System.Threading;

namespace Brightgroove.BasketsGame.Players
{
    public abstract class Player
    {
        private readonly string _name;

        private static readonly Dictionary<PlayerType, Func<string, Player>> _players = new Dictionary<PlayerType, Func<string, Player>>
        {
            { PlayerType.Memory, name => new MemoryPlayer(name) },
            { PlayerType.Random, name => new RandomPlayer(name) },
            { PlayerType.Thorough, name => new ThoroughPlayer(name) },
            { PlayerType.CheaterRandom, name => new Cheater(new RandomPlayer(name)) },
            { PlayerType.CheaterThorough, name => new Cheater(new ThoroughPlayer(name)) }
        };

        public string Name { get { return _name; } }

        public static Player CreatePlayer(string name, PlayerType playerType)
        {
            if(_players.ContainsKey(playerType))
            {
                return _players[playerType](name);
            }

            throw new ArgumentException("Player type is not supported");
        }

        public Player(string name)
        {
            _name = name;
        }

        public abstract int GuessAlgorithm(Game game);

        public void GuessNumber(Game game)
        {
            int guess = GuessAlgorithm(game);
            Console.WriteLine($"{Name} guess is: {guess}");
            game.MakeAttempt(guess, this);
        }

        public void PutOnHold(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}