namespace FishingNet
{
    public class Fish
    {
        private string fishType;
        private double length;
        private double weight;

        public Fish(string fishType, double length, double weight)
        {
            this.fishType = fishType;
            this.length = length;
            this.weight = weight;
        }

        public string FishType { get => fishType; set => fishType = value; }
        public double Length { get => length; set => length = value; }
        public double Weight { get => weight; set => weight = value; }
        public bool Released { get; set; } = false;

        public override string ToString()
        {
            return $"There is a {fishType}, {length} cm. long, and {weight} gr. in weight.";
        }
    }
}
