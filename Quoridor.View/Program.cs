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

            Console.WriteLine(game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(0, 2), new Vector2(1, 2))));
            // Console.WriteLine(game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(4, 2), new Vector2(3, 2))));

            while (true)
            {
                DisplayField(game);
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
                            // Cell firstCell = game.Field.Cells[firstPoint.x, firstPoint.y];
                            // Cell secondCell = game.Field.Cells[secondPoint.x, secondPoint.y];

                            // if (firstCell.UpperCell == secondCell ||
                            //     firstCell.LowerCell == secondCell ||
                            //     firstCell.LeftCell == secondCell ||
                            //     firstCell.RightCell == secondCell)
                            {
                                turnResult =
                                    game.TryAddDeprecatedPath(new DeprecatedPath(new Vector2(x1, y1), new Vector2(x2, y2)));
                            }
                        }
                    }
                    catch
                    {
                        turnResult = false;
                    }
                }
                else if (turnInput == "3")
                {
                    Console.WriteLine("Deprecated paths:");
                    game.DeprecatedPaths.ForEach(p => Console.WriteLine(p));
                    Console.WriteLine();
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
                            int reventedC = c / 2;

                            var fp = new Vector2(r, reventedC);
                            var sp = new Vector2(r + 1, reventedC);

                            if (game.DoesDeprecatedPathExist(new DeprecatedPath(fp, sp)))
                            {
                                Console.WriteLine(reventedC);
                                Console.WriteLine("fp = " + fp);
                                Console.WriteLine("sp = " + sp);
                                
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