using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01.BakeryShop
{
    public class Program
    {
        static void Main(string[] args)
        {
            Queue<double> water = new Queue<double>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray());
            Stack<double> flour = new Stack<double>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray());
            Dictionary<string, double[]> products = new Dictionary<string, double[]>()
            {
                {"Croissant",   new double[2]{50,50}  },
                {"Muffin",      new double[2]{40,60}  },
                {"Baguette",    new double[2]{30,70}  },
                {"Bagel",       new double[2]{20,80}  }
            };
            Dictionary<string, int> bakedProducts = new Dictionary<string, int>()
            {
                {"Croissant",   0  },
                {"Muffin",      0  },
                {"Baguette",    0  },
                {"Bagel",       0  }
            };

            while (water.Count > 0 && flour.Count > 0)
            {
                double waterQuantity = water.Peek();
                double flourQuantity = flour.Peek();
                double waterRatio = (waterQuantity / (waterQuantity + flourQuantity)) *100;
                double flourRatio = (flourQuantity / (waterQuantity + flourQuantity)) *100;
                bool productBaked = false;

                foreach (KeyValuePair<string, double[]> product in products)
                {
                    if (product.Value[0] == waterRatio && product.Value[1] == flourRatio)
                    {
                        bakedProducts[product.Key]++;
                        water.Dequeue();
                        flour.Pop();
                        productBaked = true;
                        break;
                    }
                }

                if (!productBaked)
                {
                    flour.Push(flour.Pop() - waterQuantity);
                    water.Dequeue();
                    bakedProducts["Croissant"]++;
                }
            }

            foreach (KeyValuePair<string, int> product in bakedProducts.Where(x => x.Value > 0).OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{product.Key}: {product.Value}");
            }

            Console.WriteLine
                (
                    water.Count > 0
                    ? $"Water left: {string.Join(", ", water)}"
                    : "Water left: None"
                );

            Console.WriteLine
                (
                    flour.Count > 0
                    ? $"Flour left: {string.Join(", ", flour)}"
                    : "Flour left: None"
                );
        }
    }
}
