using System;
using Monopolio.Models;
using Monopolio.Views;

namespace Monopolio.Controllers
{
    public class CommandController
    {
        private readonly GameController game;
        private readonly GameState state;

        public CommandController(GameState gameState)
        {
            state = gameState;
            game = new GameController(state);
        }

        public string Execute(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return string.Empty;

            string[] parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToUpper();

            switch (command)
            {
                case "RJ":
                    return parts.Length == 2
                        ? game.RegisterPlayer(parts[1])
                        : "Instrução inválida.";

                case "LJ":
                    return game.ListPlayers();

                case "IJ":
                    if (parts.Length < 3)
                        return "Instrução inválida.";

                    string[] names = new string[parts.Length - 1];
                    for (int i = 1; i < parts.Length; i++)
                        names[i - 1] = parts[i];

                    return game.StartGame(names);

                case "LD":
                    if (parts.Length == 2)
                        return game.LaunchDice(parts[1]);

                    if (parts.Length == 4 &&
                        int.TryParse(parts[2], out int d1) &&
                        int.TryParse(parts[3], out int d2))
                        return game.LaunchDice(parts[1], d1, d2);

                    return "Instrução inválida.";

                case "CE":
                    return parts.Length == 2
                        ? game.BuySpace(parts[1])
                        : "Instrução inválida.";

                case "TT":
                    return parts.Length == 2
                        ? game.EndTurn(parts[1])
                        : "Instrução inválida.";

                case "PA":
                    return parts.Length == 2
                        ? game.PayRent(parts[1])
                        : "Instrução inválida.";

                case "CC":
                    return parts.Length == 3
                        ? game.BuyHouse(parts[1], parts[2])
                        : "Instrução inválida.";

                case "TC":
                    return parts.Length == 2
                        ? game.DrawCard(parts[1])
                        : "Instrução inválida.";

                case "DJ":
                    BoardPrinter.Print(game.GetBoard(), state);
                    return string.Empty;

                default:
                    return "Instrução inválida.";
            }
        }
    }
}
