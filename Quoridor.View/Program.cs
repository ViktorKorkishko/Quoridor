using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.ExceptionServices;

namespace Quoridor.View
{
    public static class Program
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

            game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(0, 2), new Vector2(1, 2)));
            game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(0, 2), new Vector2(0, 3)));


            game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(4, 2), new Vector2(3, 2)));

            while (true)
            {
                string turnInfoMessage = String.Empty;

                DisplayField(game);

                Console.WriteLine("Deprecated paths:");
                game.DeprecatedPaths.ForEach(p => Console.WriteLine(p));
                Console.WriteLine();

                Console.WriteLine(game.GetCurrentPlayer().Name + " turns.");

                Console.WriteLine("What would you like to do?");

                Console.WriteLine("1. Move.");
                Console.WriteLine("2. Set wall.");

                Console.Write("Your choice: ");
                string turnInput = Console.ReadLine();

                bool turnResult = false;

                if (turnInput == "1" || turnInput == "2")
                {
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
                                goto default;

                            case "s":
                                turnResult = game.TryMoveCurrentPlayer(Direction.Down);
                                goto default;

                            case "a":
                                turnResult = game.TryMoveCurrentPlayer(Direction.Left);
                                goto default;

                            case "d":
                                turnResult = game.TryMoveCurrentPlayer(Direction.Right);
                                goto default;

                            default:
                                turnInfoMessage = "You've made incorrect move. Turn again.";
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

                            DeprecatedPath deprecatedPath = new DeprecatedPath(firstPoint, secondPoint);

                            if (!game.DoesDeprecatedPathExist(deprecatedPath))
                            {
                                if (!game.Field.IsOutOfRange(firstPoint) && !game.Field.IsOutOfRange(secondPoint))
                                {
                                    // check if cells are close
                                    if (Vector2.Distance(firstPoint, secondPoint) - 1f < 0.000000001f)
                                    {
                                        Console.WriteLine(turnResult =
                                            game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(x1, y1),
                                                new Vector2(x2, y2))));
                                    }
                                }
                                else
                                {
                                    turnInfoMessage = "One of entered points is out of field range.";
                                }
                            }
                            else
                            {
                                turnInfoMessage = $"Depracated path {deprecatedPath} already exists.";
                            }
                        }
                        catch
                        {
                            turnResult = false;
                            turnInfoMessage = "Please, enter integer numbers for coordinates.";
                        }
                    }
                }
                else
                {
                    turnInfoMessage = "Please, choose correct menu item.";
                }
                
                if (turnResult)
                {
                    game.SwitchPlayer();
                }
                else
                {
                    Console.WriteLine(turnInfoMessage);
                }
            }
        }

        private static void DisplayField(Quoridor game)
        {
            Cell[,] field = game.Field.Cells;
            Player firstPlayer = game.FirstPlayer;
            Player secondPlayer = game.SecondPlayer;

            int rowsCount = field.GetLength(0);
            int columsCount = field.GetLength(1);

            char separationChar = '-';

            for (int c = 0; c < columsCount * 2 + 1; c++)
            {
                Console.Write(separationChar);
            }

            Console.WriteLine();

            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < columsCount; c++)
                {
                    separationChar = '|';

                    if (c == 0)
                    {
                        Console.Write(separationChar);
                    }

                    if (!game.Field.IsOutOfRange(new Vector2(r, c + 1)))
                    {
                        if (game.DoesDeprecatedPathExist(new DeprecatedPath(new Vector2(r, c), new Vector2(r, c + 1))))
                        {
                            separationChar = '■';
                        }
                    }

                    Player playerOver = field[r, c].PlayerOver;
                    if (playerOver != null)
                    {
                        if (playerOver == firstPlayer)
                        {
                            Console.Write("1" + separationChar);
                        }
                        else if (playerOver == secondPlayer)
                        {
                            Console.Write("2" + separationChar);
                        }
                    }
                    else
                    {
                        Console.Write(" |");
                    }
                }

                Console.WriteLine();

                for (int c = 0; c < columsCount * 2 + 1; c++)
                {
                    separationChar = '-';
                    if (r != rowsCount - 1)
                    {
                        if (c % 2 == 1)
                        {
                            var fp = new Vector2(r, c / 2);
                            var sp = new Vector2(r + 1, c / 2);

                            if (game.DoesDeprecatedPathExist(new DeprecatedPath(fp, sp)))
                            {
                                separationChar = '■';
                            }
                        }
                    }

                    Console.Write(separationChar);
                }

                Console.WriteLine();
            }
        }
    }
}