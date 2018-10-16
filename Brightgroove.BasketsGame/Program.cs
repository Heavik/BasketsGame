using Brightgroove.BasketsGame.Players;
using System;

namespace Brightgroove.BasketsGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            game.Start();

            var p1 = Player.CreatePlayer("Random", PlayerType.Random);
            var p2 = Player.CreatePlayer("Thorough", PlayerType.Thorough);
            var p3 = Player.CreatePlayer("Memory", PlayerType.Memory);
            var p4 = Player.CreatePlayer("Cheater Random", PlayerType.CheaterRandom);
            var p5 = Player.CreatePlayer("Cheater Thorough", PlayerType.CheaterThorough);

            Console.WriteLine("RealNumber is: {0}", game.RealNumber);

            while(!game.IsStopped)
            {
                p1.GuessNumber(game);
                p2.GuessNumber(game);
                p3.GuessNumber(game);
                p4.GuessNumber(game);
                p5.GuessNumber(game);
            }
        }

        private static void StartPlaying(Player player)
        {

        }
    }
}