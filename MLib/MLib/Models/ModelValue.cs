using DRLib.Structures;

namespace MLib.Models
{
    public class ModelValue
    {
        public string Name { get; private set; }

        public double Min => Bounds.left;
        public double Max => Bounds.right;

        public LineBounds<double> Bounds { get; private set; }

        public ModelValue(string name, double min, double max)
        {
            Name = name;
            Bounds = new LineBounds<double>(min, max);
        }
    }
}