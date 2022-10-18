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
        public string Project { get; private set; }
        public int NeededRenovators { get => neededRenovators - renovators.Count; }
        public int Count { get => renovators.Count; }
        IReadOnlyCollection<Renovator> Renovators => renovators.Values;

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
                return "Invalid renovator's Rate.";
            }

            renovators.Add(renovator.Name, renovator);

            return $"Successfully added {renovator.Name} to the catalog.";
        }

        public bool RemoveRenovator(string name) => renovators.Remove(name);

        public int RemoveRenovatorBySpecialty(string type)
        {
            List<string> remove = new List<string>();

            foreach (Renovator renovator in Renovators)
            {
                if (renovator.Type == type)
                {
                    remove.Add(renovator.Name);
                }
            }

            for (int i = 0; i < remove.Count; i++)
            {
                renovators.Remove(remove[i]);
            }

            return remove.Count;
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

        public List<Renovator> PayRenovators(int days)
        {
            List<Renovator> output = new List<Renovator>();

            foreach (Renovator renovator in Renovators)
            {
                if (renovator.Days >= days)
                {
                    output.Add(renovator);
                }
            }

            return output;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Renovators available for Project {Project}:");

            foreach (Renovator renovator in Renovators.Where(x => x.Hired==false))
            {
                sb.AppendLine($"{renovator}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
