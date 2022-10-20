using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _02.BeaverAtWork
{
    public class Program
    {
        private static int pondSize;
        private static char[,] pond;
        private static Stack<char> branches = new Stack<char>();
        private static int currentRow = -1;
        private static int currentCol = -1;
        private static int remainingBranches = 0;
        private static Dictionary<string, int[]> directions = new Dictionary<string, int[]>()
            {
                {"up",      new int[2]  {-1,0} },
                {"down",    new int[2]  {+1,0} },
                {"left",    new int[2]  {0,-1} },
                {"right",   new int[2]  {0,+1} }
            };

        static void Main(string[] args)
        {
            pondSize = int.Parse(Console.ReadLine());

            pond = GeneratePond();

            string input = Console.ReadLine();

            while (input != "end" && remainingBranches > 0)
            {
                int newRow = currentRow + directions[input][0];
                int newCol = currentCol + directions[input][1];

                if (OutOfPond(newRow, newCol))
                {
                    RemoveBranch();
                    input = Console.ReadLine();
                    continue;
                }

                pond[currentRow, currentCol] = '-';
                currentRow = newRow;
                currentCol = newCol;

                if (char.IsLower(pond[currentRow, currentCol]))
                {
                    CollectBranch();
                }
                else if (pond[currentRow, currentCol] == 'F')
                {
                    pond[currentRow, currentCol] = '-';
                    SwimUnderwater(input);

                    if (char.IsLower(pond[currentRow, currentCol]))
                    {
                        CollectBranch();
                    }
                }

                pond[currentRow, currentCol] = 'B';

                input = Console.ReadLine();
            }

            if (remainingBranches == 0)
            {
                Console.WriteLine($"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches.Reverse())}.");
            }
            else
            {
                Console.WriteLine($"The Beaver failed to collect every wood branch. There are {remainingBranches} branches left.");
            }

            PrintPond();
        }

        private static char[,] GeneratePond()
        {
            char[,] output = new char[pondSize, pondSize];
            
            for (int i = 0; i < pondSize; i++)
            {
                char[] row = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();

                for (int j = 0; j < pondSize; j++)
                {
                    output[i, j] = row[j];

                    if (row[j] == 'B')
                    {
                        currentRow = i;
                        currentCol = j;
                    }
                    else if (char.IsLower(row[j]))
                    {
                        remainingBranches++;
                    }
                }
            }

            return output;
        }

        private static void PrintPond()
        {
            for (int i = 0; i < pondSize; i++)
            {
                StringBuilder sb = new StringBuilder();

                for (int j = 0; j < pondSize; j++)
                {
                    sb.Append($"{pond[i, j]} ");
                }

                Console.Write(sb.ToString().TrimEnd());
                Console.WriteLine();
            }
        }

        private static void CollectBranch()
        {
            branches.Push(pond[currentRow, currentCol]);
            remainingBranches--;
        }

        private static void SwimUnderwater(string direction)
        {
            int firstIndex = direction == "up" || direction == "left" ? pondSize - 1 : 0;
            int lastIndex = direction == "up" || direction == "left" ? 0 : pondSize - 1;
            int movement = direction == "up" || direction == "down" ? currentRow : currentCol;

            if (movement != lastIndex)
            {
                movement = lastIndex;
            }
            else
            {
                movement = firstIndex;
            }

            if (movement == currentRow)
            {
                currentCol = movement;
            }
            else
            {
                currentRow = movement;
            }

        }

        private static void RemoveBranch()
        {
            if (branches.Count > 0)
            {
                branches.Pop();
            }
        }

        private static bool OutOfPond(int row, int col) => row < 0 || row > pond.GetLength(0) - 1 || col < 0 || col > pond.GetLength(1) - 1 ? true : false;
    }
}
