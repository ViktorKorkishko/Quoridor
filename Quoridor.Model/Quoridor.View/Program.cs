using System;
using System.Diagnostics;

namespace Quoridor.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Quoridor!");
            
            Console.Write('—');
            
            Console.Write("Enter field horizontal size: ");
            int x = int.Parse(Console.ReadLine());
            
            Console.Write("Enter field vertical size: ");
            int y = int.Parse(Console.ReadLine());

            var game = new Quoridor(x, y);
            var field = game.Field.Field1;
            
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
                    // put * if player is there
                    Console.Write(" |");
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