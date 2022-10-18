using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _01.MealPlan
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Queue<string> meals = new Queue<string>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            Stack<int> days = new Stack<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            Dictionary<string, int> menu = new Dictionary<string, int>()
            {
                {"salad",   350 },
                {"soup",    490 },
                {"pasta",   680 },
                {"steak",   790 }
            };
            int count = 0;

            while (days.Count > 0 && meals.Count > 0)
            {
                int calories = menu[meals.Dequeue()];
                int currentDay = days.Pop();

                if (currentDay - calories <= 0)
                {
                    if (days.Count > 0)
                    {
                        int nextDay = days.Pop();
                        days.Push(nextDay - Math.Abs(currentDay - calories));
                    }
                }
                else
                {
                    days.Push(currentDay - calories);
                }

                count++;
            }

            if (meals.Count == 0)
            {
                Console.WriteLine($"John had {count} meals.");
                Console.WriteLine($"For the next few days, he can eat {string.Join(", ",days)} calories.");
            }
            else
            {
                Console.WriteLine($"John ate enough, he had {count} meals.");
                Console.WriteLine($"Meals left: {string.Join(", ",meals)}.");
            }
        }
    }
}
