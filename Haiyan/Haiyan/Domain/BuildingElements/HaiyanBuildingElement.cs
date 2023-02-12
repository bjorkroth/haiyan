using Haiyan.Domain.Geometry;
using Haiyan.Domain.Materials;

namespace Haiyan.Domain.BuildingElements
{
    public class HaiyanBuildingElement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string GUID { get; set; }
        public string IfcGuid { get; set; }
        public string BoverketProductCategory { get; set; }
        public HaiyanMaterial Material { get; set; }
        public HaiyanGeometry Geometry { get; set; }
    }
}
