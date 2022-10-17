using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01.BaristaContest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Queue<int> coffeeQiantity = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            Stack<int> milkQuantity = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            Dictionary<string, int> result = new Dictionary<string, int>();
            Dictionary<string, int> coffeeDrinks = new Dictionary<string, int> 
            {
                {"Cortado", 50 },
                {"Espresso", 75 },
                {"Capuccino", 100 },
                {"Americano", 150 },
                {"Latte", 200 }
            };

            while (coffeeQiantity.Count > 0 && milkQuantity.Count > 0)
            {
                bool isEqual = false;

                foreach (KeyValuePair<string, int> coffeeDrink in coffeeDrinks)
                {
                    if (coffeeDrink.Value == coffeeQiantity.Peek() + milkQuantity.Peek())
                    {
                        if (!result.ContainsKey(coffeeDrink.Key))
                        {
                            result.Add(coffeeDrink.Key, 0);
                        }

                        result[coffeeDrink.Key]++;

                        coffeeQiantity.Dequeue();
                        milkQuantity.Pop();

                        isEqual = true;
                        break;
                    }
                }

                if (!isEqual)
                {
                    if (coffeeQiantity.Any())
                    {
                        coffeeQiantity.Dequeue();
                    }
                    if (milkQuantity.Any())
                    {
                        milkQuantity.Push(milkQuantity.Pop() - 5);
                    }
                }
            }

            Console.WriteLine(
                coffeeQiantity.Count == 0 && milkQuantity.Count == 0
                ? "Nina is going to win! She used all the coffee and milk!"
                : "Nina needs to exercise more! She didn't use all the coffee and milk!");

            Console.WriteLine(
                coffeeQiantity.Count == 0
                ? "Coffee left: none"
                : $"Coffee left: {string.Join(", ", coffeeQiantity)}");

            Console.WriteLine(
                milkQuantity.Count == 0
                ? "Milk left: none"
                : $"Milk left: {string.Join(", ", milkQuantity)}");

            foreach (KeyValuePair<string,int> drink in result.OrderBy(x=>x.Value).ThenByDescending(x=>x.Key))
            {
                Console.WriteLine($"{drink.Key}: {drink.Value}");
            }
        }
    }
}
