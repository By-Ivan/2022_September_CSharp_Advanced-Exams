using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renovators
{
    public class Catalog
    {
        private Dictionary<string, Renovator> renovators = new Dictionary<string, Renovator>();
        private int neededRenovators;

        public Catalog(string name, int neededRenovators, string project)
        {
            Name = name;
            this.neededRenovators = neededRenovators;
            Project = project;
        }

        public string Name { get; private set; }
        public int NeededRenovators { get => neededRenovators - Renovators.Count; }
        public string Project { get; private set; }
        IReadOnlyCollection<Renovator> Renovators => renovators.Values;
        public int Count => Renovators.Count;

        public string AddRenovator(Renovator renovator)
        {
            if (string.IsNullOrEmpty(renovator.Name) || string.IsNullOrEmpty(renovator.Type))
            {
                return "Invalid renovator's information.";
            }

            if (NeededRenovators == 0)
            {
                return "Renovators are no more needed.";
            }

            if (renovator.Rate > 350)
            {
                return "Invalid renovator's rate.";
            }

            renovators.Add(renovator.Name, renovator);

            return $"Successfully added {renovator.Name} to the catalog.";
        }

        public bool RemoveRenovator(string name) => renovators.Remove(name);

        public int RemoveRenovatorBySpecialty(string type)
        {
            int count = 0;

            foreach (Renovator renovator in Renovators.Where(x => x.Type == type))
            {
                renovators.Remove(renovator.Name);
                count++;
            }

            return count;
        }

        public Renovator HireRenovator(string name)
        {
            if (renovators.ContainsKey(name))
            {
                renovators[name].Hired = true;
                return renovators[name];
            }

            return null;
        }

        public List<Renovator> PayRenovators(int days) => new List<Renovator>(Renovators.Where(x => x.Days >= days));

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Renovators available for Project {Project}:");

            foreach (Renovator renovator in Renovators.Where(x => x.Hired == false))
            {
                sb.AppendLine($"{renovator}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
