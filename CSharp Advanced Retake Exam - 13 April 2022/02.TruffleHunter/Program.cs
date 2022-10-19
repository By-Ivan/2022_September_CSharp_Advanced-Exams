using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.TruffleHunter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> truffles = new Dictionary<char, int>()
            {
                {'B',0 },
                {'S',0 },
                {'W',0 }
            };

            char[,] forest = GenerateForest(int.Parse(Console.ReadLine()));
            int boarCount = 0;

            string input = Console.ReadLine();

            while (input != "Stop the hunt")
            {
                string[] cmd = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string command = cmd[0];
                int row = int.Parse(cmd[1]);
                int col = int.Parse(cmd[2]);

                if (row >= 0 && row < forest.GetLength(0) && col >=0 && col < forest.GetLength(1))
                {
                    switch (command)
                    {
                        case "Collect":
                            if (truffles.ContainsKey(forest[row, col]))
                            {
                                truffles[forest[row, col]]++;
                                forest[row, col] = '-';
                            }
                            break;

                        case "Wild_Boar":
                            string direction = cmd[3];

                            switch (direction)
                            {
                                case "up":
                                    for (int i = row; i >= 0; i -= 2)
                                    {
                                        if (truffles.ContainsKey(forest[i, col]))
                                        {
                                            boarCount++;
                                            forest[i, col] = '-';
                                        }
                                    }
                                    break;

                                case "down":
                                    for (int i = row; i < forest.GetLength(0); i += 2)
                                    {
                                        if (truffles.ContainsKey(forest[i, col]))
                                        {
                                            boarCount++;
                                            forest[i, col] = '-';
                                        }
                                    }
                                    break;

                                case "left":
                                    for (int i = col; i >= 0; i -= 2)
                                    {
                                        if (truffles.ContainsKey(forest[row, i]))
                                        {
                                            boarCount++;
                                            forest[row, i] = '-';
                                        }
                                    }
                                    break;

                                case "right":
                                    for (int i = col; i < forest.GetLength(1); i += 2)
                                    {
                                        if (truffles.ContainsKey(forest[row, i]))
                                        {
                                            boarCount++;
                                            forest[row, i] = '-';
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }

                input = Console.ReadLine();
            }

            Console.WriteLine($"Peter manages to harvest {truffles['B']} black, {truffles['S']} summer, and {truffles['W']} white truffles.");
            Console.WriteLine($"The wild boar has eaten {boarCount} truffles.");
            PrintForest(forest);
        }

        private static void PrintForest(char[,] forest)
        {
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    Console.Write($"{forest[i,j]} ");
                }
                Console.Write("".TrimEnd());
                Console.WriteLine();
            }
        }

        private static char[,] GenerateForest(int n)
        {
            char[,] output = new char[n, n];

            for (int i = 0; i < n; i++)
            {
                char[] row = Console.ReadLine().Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();

                for (int j = 0; j < n; j++)
                {
                    output[i,j] = row[j];
                }
            }

            return output;
        }
    }
}
