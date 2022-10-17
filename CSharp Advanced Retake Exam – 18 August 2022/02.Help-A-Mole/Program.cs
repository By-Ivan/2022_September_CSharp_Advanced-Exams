using System;
using System.Collections.Generic;

namespace _02.Help_A_Mole
{
    public class Program
    {
        public static char[,] field;
        public static int currentRow = 0;
        public static int currentCol = 0;
        public static List<int[]> specialPositions = new List<int[]>();
        public static int points = 0;
        public static Dictionary<string, int[]> directions = new Dictionary<string, int[]>()
            {
                {"up",      new int[2] { -1,0 } },
                {"down",    new int[2] { +1,0 } },
                {"right",    new int[2] { 0,+1 } },
                {"left",    new int[2] { 0,-1 } }
            };

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            
            GenerateField(n);

            string input = Console.ReadLine();

            while (input != "End" && points < 25)
            {
                string direction = input.Trim();

                if (directions.ContainsKey(direction))
                {
                    Move(direction);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine
                (
                    points < 25 
                    ? "Too bad! The Mole lost this battle!"
                    : "Yay! The Mole survived another game!"
                );

            Console.WriteLine
                (
                    points < 25
                    ? $"The Mole lost the game with a total of {points} points."
                    : $"The Mole managed to survive with a total of {points} points."
                );

            PrintField(n);
        }

        private static void GenerateField(int n)
        {
            field = new char[n, n];

            for (int i = 0; i < n; i++)
            {
                char[] row = Console.ReadLine().ToCharArray();

                for (int j = 0; j < n; j++)
                {
                    field[i, j] = row[j];

                    if (row[j] == 'M')
                    {
                        currentRow = i;
                        currentCol = j;
                    }
                    else if (row[j] == 'S')
                    {
                        specialPositions.Add(new int[2] { i, j });
                    }
                }
            }
        }

        private static void Move(string direction)
        {
            int[] newPosition = new int[2]
                    {
                        currentRow + directions[direction][0],
                        currentCol + directions[direction][1]
                    };

            if (newPosition[0] < 0 || newPosition[1] < 0 || newPosition[0] >= field.GetLength(0) || newPosition[1] >= field.GetLength(1))
            {
                Console.WriteLine("Don't try to escape the playing field!");
            }
            else if (field[newPosition[0], newPosition[1]] == 'S')
            {
                field[currentRow, currentCol] = '-';

                foreach (int[] specialPosition in specialPositions)
                {
                    if (specialPosition[0] != newPosition[0] || specialPosition[1] != newPosition[1])
                    {
                        field[specialPosition[0], specialPosition[1]] = 'M';
                        currentRow = specialPosition[0];
                        currentCol = specialPosition[1];
                    }
                    else
                    {
                        field[specialPosition[0], specialPosition[1]] = '-';
                    }
                }

                points -= 3;
            }
            else if (char.IsDigit(field[newPosition[0], newPosition[1]]))
            {
                points += field[newPosition[0], newPosition[1]] - '0';

                field[currentRow, currentCol] = '-';
                field[newPosition[0], newPosition[1]] = 'M';
                currentRow = newPosition[0];
                currentCol = newPosition[1];
            }
            else
            {
                field[currentRow, currentCol] = '-';
                field[newPosition[0], newPosition[1]] = 'M';
                currentRow = newPosition[0];
                currentCol = newPosition[1];
            }
        }

        private static void PrintField(int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
