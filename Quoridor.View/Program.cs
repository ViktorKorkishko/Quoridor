using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Quoridor.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine("Welcome to Quoridor!");
            //
            // Console.Write('—');
            //
            // Console.Write("Enter first player name: ");
            // string firstPlayerName = Console.ReadLine();
            // Console.WriteLine("You will be marked as 1 in the game field.");
            //
            // Console.Write("Enter second player name: ");
            // string secondPlayerName = Console.ReadLine();
            // Console.WriteLine("You will be marked as 2 in the game field.");

            Console.Write("Enter field horizontal size: ");
            // int y = int.Parse(Console.ReadLine());

            Console.Write("Enter field vertical size: ");
            // int x = int.Parse(Console.ReadLine());

            var firstPlayer = new Player("Player1");
            var secondPlayer = new Player("Player2");

            var game = new Quoridor(3, 5, firstPlayer, secondPlayer);

            while (true)
            {
                DisplayField(game.Field.Cells, firstPlayer, secondPlayer);
                Console.WriteLine(game.GetCurrentPlayer().Name + " turns");
                
                Console.WriteLine("First player position: " + game.GetCurrentPlayer().Position);
                game.SwitchPlayer();
                Console.WriteLine("Second player position: " + game.GetCurrentPlayer().Position);
                game.SwitchPlayer();
                
                Console.WriteLine("What would you like to do?");

                int index = int.Parse(Console.ReadLine());
                
                switch (index)
                {
                    case 1:
                        game.TryMovePlayer(Direction.Up);
                        break;
                    case 2:
                        game.TryMovePlayer(Direction.Down);
                        break;
                    case 3:
                        game.TryMovePlayer(Direction.Left);
                        break;
                    case 4:
                        game.TryMovePlayer(Direction.Right);
                        break;
                    
                    default:
                        break;
                }
                
                game.SwitchPlayer();
            }
        }

        private static void DisplayField(Cell[,] field, Player firstPlayer, Player secondPlayer)
        {
            Console.WriteLine();
            for (int k = 0; k < field.GetLength(1) * 2 + 1; k++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write("|");
                    }

                    Player playerOver = field[i, j].PlayerOver;
                    if (playerOver != null)
                    {
                        if (playerOver == firstPlayer)
                        {
                            Console.Write("1|");
                        }
                        else if (playerOver == secondPlayer)
                        {
                            Console.Write("2|");
                        }
                    }
                    else
                    {
                        Console.Write(" |");
                    }
                }

                Console.WriteLine();

                for (int k = 0; k < field.GetLength(1) * 2 + 1; k++)
                {
                    Console.Write("-");
                }

                Console.WriteLine();
            }
        }
    }
}