using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.ExceptionServices;

namespace Quoridor.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quoridor!");

            // Console.Write("Enter first player name: ");
            // string firstPlayerName = Console.ReadLine();
            // Console.WriteLine("You will be marked as 1 in the game field.");
            //
            // Console.Write("Enter second player name: ");
            // string secondPlayerName = Console.ReadLine();
            // Console.WriteLine("You will be marked as 2 in the game field.");

            // Console.Write("Enter field horizontal size: ");
            // int y = int.Parse(Console.ReadLine());
            int y = 5;
            
            // Console.Write("Enter field vertical size: ");
            // int x = int.Parse(Console.ReadLine());
            int x = 5;

            var firstPlayer = new Player("firstPlayerName");
            var secondPlayer = new Player("secondPlayerName");

            var game = new Quoridor(x, y, firstPlayer, secondPlayer);

            while (true)
            {
                DisplayField(game.Field.Cells, firstPlayer, secondPlayer);
                Console.WriteLine(game.GetCurrentPlayer().Name + " turns");

                Console.WriteLine("What would you like to do?");
                
                Console.WriteLine("1. Move");
                Console.WriteLine("2. Set wall");
                
                Console.Write("Your choice: ");
                string turnInput = Console.ReadLine();

                bool turnResult = false;

                if (turnInput == "1")
                {
                    Console.WriteLine("w to move up");
                    Console.WriteLine("a to move left");
                    Console.WriteLine("s to move down");
                    Console.WriteLine("d to move right");

                    Console.Write("Your choice: ");
                    string directionInput = Console.ReadLine();
                    switch (directionInput)
                    {
                        case "w":
                            turnResult = game.TryMoveCurrentPlayer(Direction.Up);
                            break;

                        case "s":
                            turnResult = game.TryMoveCurrentPlayer(Direction.Down);
                            break;

                        case "a":
                            turnResult = game.TryMoveCurrentPlayer(Direction.Left);
                            break;

                        case "d":
                            turnResult = game.TryMoveCurrentPlayer(Direction.Right);
                            break;
                    }
                }
                else if (turnInput == "2")
                {
                    try
                    {
                        Console.Write("Enter x1: ");
                        int.TryParse(Console.ReadLine(), out var x1);

                        Console.Write("Enter y1: ");
                        int.TryParse(Console.ReadLine(), out var y1);

                        Console.Write("Enter x2: ");
                        int.TryParse(Console.ReadLine(), out var x2);

                        Console.Write("Enter y2: ");
                        int.TryParse(Console.ReadLine(), out var y2);

                        Vector2 firstPoint = new Vector2(x1, y1);
                        Vector2 secondPoint = new Vector2(x2, y2);

                        if (!game.Field.IsOutOfRange(firstPoint) && !game.Field.IsOutOfRange(secondPoint))
                        {
                            Cell firstCell = game.Field.Cells[firstPoint.x, firstPoint.y];
                            Cell secondCell = game.Field.Cells[secondPoint.x, secondPoint.y];
                            
                            // if (firstCell.UpperCell == secondCell ||
                            //     firstCell.LowerCell == secondCell ||
                            //     firstCell.LeftCell == secondCell ||
                            //     firstCell.RightCell == secondCell)
                            {
                                turnResult = game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(x1, y1), new Vector2(x2, y2)));
                            }
                        }
                    }
                    catch
                    {
                        turnResult = false;
                    }
                }
                else if(turnInput == "3")
                {
                    Console.WriteLine("Deprecated paths");
                    foreach (var path in game.DeprecatedPaths)
                    {
                        Console.WriteLine(path.FirstPoint);
                    }
                }

                if (turnResult)
                {
                    game.SwitchPlayer();
                }
                else
                {
                    Console.WriteLine("You've made incorrect move. Turn again.");
                }
            }
        }

        private static void DisplayField(Cell[,] field, Player firstPlayer, Player secondPlayer)
        {
            int rowsCount = field.GetLength(0);
            int columsCount = field.GetLength(1);

            for (int k = 0; k < columsCount * 2 + 1; k++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columsCount; j++)
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

                for (int k = 0; k < columsCount * 2 + 1; k++)
                {
                    Console.Write("-");
                }

                Console.WriteLine();
            }
        }
    }
}