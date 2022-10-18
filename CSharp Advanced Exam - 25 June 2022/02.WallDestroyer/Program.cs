using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace _02.WallDestroyer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[,] wall = new char[n, n];
            int currentRow = -1;
            int currentCol = -1;
            int holes = 0;
            int rods = 0;
            bool firstMove = true;
            bool electrocuted = false;

            Dictionary<string, int[]> directions = new Dictionary<string, int[]>()
            {
                {"up", new int[]{-1,0} },
                {"down", new int[]{+1,0} },
                {"left", new int[]{0,-1} },
                {"right", new int[]{0,+1} }
            };

            for (int i = 0; i < n; i++)
            {
                string row = Console.ReadLine();

                for (int j = 0; j < n; j++)
                {
                    wall[i, j] = row[j];

                    if (wall[i, j] == 'V')
                    {
                        currentRow = i;
                        currentCol = j;
                    }
                }
            }

            string input = Console.ReadLine();

            while (input != "End")
            {
                int newRow = currentRow + directions[input][0];
                int newCol = currentCol + directions[input][1];

                if (newRow < 0 || newRow >= n || newCol < 0 || newCol >= n)
                {
                    input = Console.ReadLine();
                    continue;
                }

                switch (wall[newRow, newCol])
                {
                    case 'R':
                        Console.WriteLine("Vanko hit a rod!");
                        rods++;
                        newRow = currentRow;
                        newCol = currentCol;
                        break;

                    case 'C':
                        wall[newRow, newCol] = 'E';
                        electrocuted = true;
                        break;

                    case '*':
                        Console.WriteLine($"The wall is already destroyed at position [{newRow}, {newCol}]!");
                        break;

                    case '-':
                        wall[newRow, newCol] = '*';
                        break;
                }

                if (firstMove)
                {
                    wall[currentRow, currentCol] = '*';
                }

                currentRow = newRow;
                currentCol = newCol;


                if (electrocuted)
                {
                    holes = CountHoles(wall);
                    Console.WriteLine($"Vanko got electrocuted, but he managed to make {++holes} hole(s).");
                    PrintWall(wall);
                    return;
                }

                input = Console.ReadLine();
            }

            holes = CountHoles(wall);
            wall[currentRow, currentCol] = 'V';
            Console.WriteLine($"Vanko managed to make {holes} hole(s) and he hit only {rods} rod(s).");
            PrintWall(wall);

        }

        private static int CountHoles(char[,] wall)
        {
            int holes = 0;

            for (int i = 0; i < wall.GetLength(0); i++)
            {
                for (int j = 0; j < wall.GetLength(1); j++)
                {
                    if (wall[i, j] == '*')
                    {
                        holes++;
                    }
                }
            }

            return holes;
        }

        public static void PrintWall(char[,] wall)
        {
            for (int i = 0; i < wall.GetLength(0); i++)
            {
                for (int j = 0; j < wall.GetLength(1); j++)
                {
                    Console.Write(wall[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
