using Brightgroove.BasketsGame.Players;
using System;

namespace Brightgroove.BasketsGame.UI
{
    public static class UserDialog
    {
        public static int ChoosePlayerNumber()
        {
            Console.Clear();
            Console.Write("Choose number of players (between 2 and 8): ");
            var input = Console.ReadLine();
            int playersNumber;

            if(!int.TryParse(input, out playersNumber))
            {
                return ChoosePlayerNumber();
            }

            if(playersNumber < Constants.MinPlayersNumber || playersNumber > Constants.MaxPlayersNumber)
            {
                return ChoosePlayerNumber();
            }

            return playersNumber;
        }

        public static Player CreatePlayer(int playerNumber)
        {
            Console.Clear();
            Console.Write("Enter the name of {0} player: ", playerNumber);
            var name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Choose player's type");
            Console.WriteLine("1. Random{0}2. Memory{0}3. Thorough{0}4. Random Cheater{0}5. Thorough Cheater", Environment.NewLine);
            var type = Console.ReadLine();
            PlayerType playerType;

            if(!Enum.TryParse<PlayerType>(type, out playerType))
            {
                return CreatePlayer(playerNumber);
            }

            if(!Enum.IsDefined(typeof(PlayerType), playerType))
            {
                return CreatePlayer(playerNumber);
            }

            return Player.CreatePlayer(name, playerType);
        }

        public static void GameBegins(Game game)
        {
            Console.Clear();
            Console.WriteLine("The Real weight of basket is: {0}", game.RealNumber);
            Console.WriteLine();
        }

        public static void GameResults(Game game)
        {
            if(!string.IsNullOrEmpty(game.Winner))
            {
                Console.WriteLine("Player {0} wins. Total number of attempts: {1}", game.Winner, game.TotalAttempts);
            }
            else
            {
                Console.WriteLine("No winner. The closest guess by {0} is {1}. Total number of attempts {2}", game.ClosestWinner.Name, game.ClosestWinner.ClosestGuess, game.TotalAttempts);
            }

            Console.WriteLine("{0}Press <Enter> to close...", Environment.NewLine);
            Console.ReadLine();
        }
    }
}