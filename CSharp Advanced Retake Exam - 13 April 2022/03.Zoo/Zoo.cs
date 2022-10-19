using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Zoo
{
    public class Zoo
    {
        private List<Animal> animals = new List<Animal>();

        public Zoo(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int Count { get => Animals.Count; }
        public int Remaining { get => Capacity - Count; }
        public IReadOnlyCollection<Animal> Animals => animals;

        public string AddAnimal(Animal animal)
        {
            if (string.IsNullOrEmpty(animal.Species))
            {
                return "Invalid animal species.";
            }

            if (animal.Diet != "herbivore" && animal.Diet != "carnivore")
            {
                return "Invalid animal diet.";
            }

            if (Remaining == 0)
            {
                return "The zoo is full.";
            }

            animals.Add(animal);
            return $"Successfully added {animal.Species} to the zoo.";
        }

        public int RemoveAnimals(string species) => animals.RemoveAll(x => x.Species == species);

        public List<Animal> GetAnimalsByDiet(string diet) => new List<Animal>(Animals.Where(x => x.Diet == diet).ToList());

        public Animal GetAnimalByWeight(double weight) => Animals.FirstOrDefault(x => x.Weight == weight);

        public string GetAnimalCountByLength(double minimumLength, double maximumLength)
        {
            int count = Animals.Where(x => x.Length >= minimumLength && x.Length <= maximumLength).Count();

            return $"There are {count} animals with a length between {minimumLength} and {maximumLength} meters.";
        }
    }
}
