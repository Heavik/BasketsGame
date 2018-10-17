using Brightgroove.BasketsGame.Players;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Brightgroove.BasketsGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            game.Start();

            //Console.WriteLine("RealNumber is: {0}", game.RealNumber);

            var players = new[]
            {
                //Player.CreatePlayer("Random", PlayerType.Random),
                Player.CreatePlayer("Thorough", PlayerType.Thorough),
                Player.CreatePlayer("Memory", PlayerType.Memory),
                Player.CreatePlayer("Cheater Random", PlayerType.CheaterRandom),
                Player.CreatePlayer("Cheater Thorough", PlayerType.CheaterThorough)
            };

            var tasksList = new List<Task>();

            foreach (var player in players)
            {
                tasksList.Add(StartPlaying(player, game));
            }

            var finish = Task.WhenAny(tasksList);
            finish.Wait();

            Console.WriteLine("Real number is: {0} Winner is: {1}, Total attempts: {2}", game.RealNumber, game.Winner, game.TotalAttempts);
        }

        private static Task StartPlaying(Player player, Game game)
        {
            return Task.Run(() =>
            {
                while (!game.IsStopped)
                {
                    player.GuessNumber(game);
                }
            });
        }
    }
}