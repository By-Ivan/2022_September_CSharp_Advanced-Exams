using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishingNet
{
    public class Net
    {
        private List<Fish> fish = new List<Fish>();

        public Net(string material, int capacity)
        {
            Material = material;
            Capacity = capacity;
        }

        public string Material { get; private set; }
        public int Capacity { get; private set; }
        public int Count { get=> Fish.Count; }

        private int RemainingFish { get => Capacity - Fish.Count; }
        public IReadOnlyList<Fish> Fish => fish;

        public string AddFish(Fish fish)
        {
            if (string.IsNullOrEmpty(fish.FishType) || fish.Length <= 0 || fish.Weight <= 0)
            {
                return "Invalid fish.";
            }

            if (RemainingFish == 0)
            {
                return "Fishing net is full.";
            }

            this.fish.Add(fish);
            return $"Successfully added {fish.FishType} to the fishing net.";
        }

        public bool ReleaseFish(double weight) => this.fish.Remove(fish.FirstOrDefault(x => x.Weight == weight));

        public Fish GetFish(string fishType) => Fish.FirstOrDefault(x => x.FishType == fishType);

        public Fish GetBiggestFish() => Fish.OrderByDescending(x => x.Length).First();

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Into the {Material}:");
            foreach (Fish fish in Fish.OrderByDescending(x=>x.Length))
            {
                sb.AppendLine(fish.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
