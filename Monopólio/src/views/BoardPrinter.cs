using System;
using Monopolio.Models;

namespace Monopolio.Views
{
    public static class BoardPrinter
    {
        public static void Print(Board board, GameState state)
        {
            if (!state.GameInProgress)
            {
                Console.WriteLine("Não existe jogo em curso.");
                return;
            }

            for (int r = 0; r < Board.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    Space space = board.Get(r, c);
                    string output = space.Name;

                    if (!string.IsNullOrEmpty(space.Owner))
                    {
                        output += " (" + space.Owner;

                        if (space.Houses > 0)
                            output += " - " + space.Houses;

                        output += ")";
                    }

                    for (int i = 0; i < state.PlayersInGame.Count; i++)
                    {
                        Player p = state.PlayersInGame[i];

                        if (p.Row == r && p.Col == c)
                            output += " " + p.Name;
                    }

                    Console.Write("[" + output + "] ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < state.PlayersInGame.Count; i++)
            {
                Player p = state.PlayersInGame[i];
                Console.WriteLine(p.Name + " - " + p.Money);
            }
        }
    }
}
