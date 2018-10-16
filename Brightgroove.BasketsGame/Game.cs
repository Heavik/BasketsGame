using Brightgroove.BasketsGame.Players;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Brightgroove.BasketsGame
{
    public class Game
    {
        private bool _isStopped = false;
        private int _attempts = 0;
        private int _realNumber;
        private HashSet<int> _triedNumbers = new HashSet<int>();

        public int RealNumber { get { return _realNumber; } }
        public ICollection<int> TriedNumbers { get { return _triedNumbers; } }
        public bool IsStopped { get { return _isStopped; } }

        public void Start()
        {
            var rnd = new Random();
            _realNumber = rnd.Next(Constants.MinBasketWeight, Constants.MaxBasketWeight);
        }

        public void MakeAttempt(int number, Player player)
        {
            if(!_isStopped && _attempts < Constants.MaxAttempts)
            {
                _triedNumbers.Add(number);
                if(_realNumber == number)
                {
                    Stop();
                }
                int delta = Math.Abs(_realNumber - number);
                if(delta > 0)
                {
                    player.PutOnHold(delta);
                }
                Interlocked.Increment(ref _attempts);
            } else
            {
                Stop();
            }
        }

        public void Stop()
        {
            _isStopped = true;
        }
    }
}