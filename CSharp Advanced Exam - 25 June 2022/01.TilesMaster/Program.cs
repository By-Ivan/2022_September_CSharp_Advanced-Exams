using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.TilesMaster
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stack<int> whiteTiles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            Queue<int> greyTiles = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            Dictionary<int, string> locations = new Dictionary<int, string>()
            {
                {40,"Sink" },
                {50,"Oven" },
                {60,"Countertop" },
                {70,"Wall" }
            };
            Dictionary<string, int> results = new Dictionary<string, int>();

            while (whiteTiles.Count > 0 && greyTiles.Count > 0)
            {
                if (whiteTiles.Peek() == greyTiles.Peek())
                {
                    int newTile = whiteTiles.Peek() + greyTiles.Peek();

                    if (locations.ContainsKey(newTile))
                    {
                        if (!results.ContainsKey(locations[newTile]))
                        {
                            results.Add(locations[newTile], 0);
                        }

                        results[locations[newTile]]++;
                        greyTiles.Dequeue();
                        whiteTiles.Pop();
                        continue;
                    }

                    greyTiles.Dequeue();
                    whiteTiles.Pop();

                    if (!results.ContainsKey("Floor"))
                    {
                        results.Add("Floor", 0);
                    }

                    results["Floor"]++;
                }
                else
                {
                    int newWhite = whiteTiles.Pop() / 2;
                    whiteTiles.Push(newWhite);
                    greyTiles.Enqueue(greyTiles.Dequeue());
                }
            }
                

            Console.WriteLine
                (
                    whiteTiles.Count == 0 
                    ? "White tiles left: none"
                    : $"White tiles left: {string.Join(", ", whiteTiles)}"
                );

            Console.WriteLine
                (
                    greyTiles.Count == 0
                    ? "Grey tiles left: none"
                    : $"Grey tiles left: {string.Join(", ", greyTiles)}"
                );

            foreach (KeyValuePair<string,int> location in results.OrderByDescending(x=>x.Value).ThenBy(x=>x.Key))
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }
    }
}
