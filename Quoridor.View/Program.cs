using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Quoridor.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine("Welcome to Quoridor!");
            //
            // Console.Write("Enter first player name: ");
            // string firstPlayerName = Console.ReadLine();
            // Console.WriteLine("You will be marked as 1 in the game field.");
            
            //Console.Write("Enter second player name: ");
            //string secondPlayerName = Console.ReadLine();
            //Console.WriteLine("You will be marked as 2 in the game field.");

            //Console.Write("Enter field horizontal size: ");
            //int y = int.Parse(Console.ReadLine());

            //Console.Write("Enter field vertical size: ");
            //int x = int.Parse(Console.ReadLine());

            var firstPlayer = new Player("Player1");
            var secondPlayer = new Player("Player2");

            var game = new Quoridor(4, 5, firstPlayer, secondPlayer);

            while (true)
            {
                Console.WriteLine("First player position: " + game.FirstPlayer.Position);
                Console.WriteLine("Second player position: " + game.SecondPlayer.Position);

                DisplayField(game.Field.Cells, firstPlayer, secondPlayer);
                Console.WriteLine(game.GetCurrentPlayer().Name + " turns");
                
                Console.WriteLine("What would you like to do?");

                char input = Console.ReadLine()[0];
                bool turnResult = false;
                switch (input)
                {
                    case 'w':
                        turnResult = game.TryMoveCurrentPlayer(Direction.Up);
                        break;
                    
                    case 's':
                        turnResult = game.TryMoveCurrentPlayer(Direction.Down);
                        break;
                    
                    case 'a':
                        turnResult = game.TryMoveCurrentPlayer(Direction.Left);
                        break;
                    
                    case 'd':
                        turnResult = game.TryMoveCurrentPlayer(Direction.Right);
                        break;
                }

                if (turnResult)
                {
                    game.SwitchPlayer();
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