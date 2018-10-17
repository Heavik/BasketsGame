using Brightgroove.BasketsGame.Players;
using Brightgroove.BasketsGame.UI;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Brightgroove.BasketsGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int playersNumber = UserDialog.ChoosePlayerNumber();
            var playersList = new List<Player>();
            for(int i = 1; i <= playersNumber; i++)
            {
                playersList.Add(UserDialog.CreatePlayer(i));
            }

            var game = new Game();

            UserDialog.GameBegins(game);

            var cts = new CancellationTokenSource(Constants.GameTimeoutMillisec);

            var tasksList = new List<Task>();

            foreach (var player in playersList)
            {
                tasksList.Add(StartPlaying(player, game, cts.Token));
            }

            Task.WaitAny(tasksList.ToArray());

            UserDialog.GameResults(game);
        }

        private static Task StartPlaying(Player player, Game game, CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!game.IsStopped)
                {
                    token.ThrowIfCancellationRequested();
                    player.GuessNumber(game);
                }
            }, token);
        }
    }
}