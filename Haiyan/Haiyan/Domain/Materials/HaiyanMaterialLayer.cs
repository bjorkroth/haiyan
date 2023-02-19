using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Geometry;

namespace Haiyan.Domain.Materials
{
    public class HaiyanMaterialLayer
    {
        public string Name { get; set; }
        public HaiyanGeometry LayerGeometry { get; set; }
        public double Thickness { get; set; }
        public string ResourceId { get; set; }
        public BuildingElementCategory BoverketProductCategory { get; set; }
        public string BoverketProductName { get; set; }
    }
}
