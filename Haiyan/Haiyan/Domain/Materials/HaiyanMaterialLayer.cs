using Haiyan.Domain.Enumerations;

namespace Haiyan.Domain.Materials
{
    public class HaiyanMaterialLayer
    {
        public string Name { get; set; }
        public double Thickness { get; set;  }
        public string ResourceId { get; set; }
        public BuildingElementCategory BoverketProductCategory { get; set; }
        public string BoverketResourceCategory { get; set; }
    }
}
