using Brightgroove.BasketsGame.Players;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Brightgroove.BasketsGame
{
    public class Game
    {
        private int _attempts = 0;
        private int _closestDelta = int.MaxValue;
        private readonly HashSet<int> _triedNumbers = new HashSet<int>();

        private readonly object _lock = new object();

        public int RealNumber { get; private set; }

        public ICollection<int> TriedNumbers => _triedNumbers;

        public bool IsStopped { get; private set; } = false;

        public int TotalAttempts => _attempts;

        public string Winner { get; private set; } = string.Empty;

        public Player ClosestWinner { get; private set; }

        public Game()
        {
            var rnd = new Random();
            RealNumber = rnd.Next(Constants.MinBasketWeight, Constants.MaxBasketWeight);
        }

        public void MakeAttempt(Player player)
        {
            _triedNumbers.Add(player.LastGuess);
            Interlocked.Increment(ref _attempts);
            int delta = Math.Abs(RealNumber - player.LastGuess);
            if (delta > 0)
            {
                UpdateClosestWinner(delta, player);
                player.PutOnHold(delta);
            }
            lock (_lock)
            {
                if (!IsStopped && _attempts < Constants.MaxAttempts)
                {
                    if (RealNumber == player.LastGuess)
                    {
                        Winner = player.Name;
                        Stop();
                    }
                }
                else
                {
                    Stop();
                }
            }
        }

        private void UpdateClosestWinner(int delta, Player player)
        {
            lock (_lock)
            {
                if (delta < _closestDelta)
                {
                    _closestDelta = delta;
                    ClosestWinner = player;
                    player.ClosestGuess = player.LastGuess;
                }
            }
        }

        private void Stop()
        {
            IsStopped = true;
        }
    }
}