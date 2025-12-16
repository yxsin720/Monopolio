using System;
using Monopolio.Models;
using Monopolio.Controllers;

namespace Monopolio
{
    class Program
    {
        static void Main(string[] args)
        {
            GameState state = new GameState();
            CommandController controller = new CommandController(state);

            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    break;

                string output = controller.Execute(line);

                if (!string.IsNullOrEmpty(output))
                    Console.WriteLine(output);
            }
        }
    }
}
