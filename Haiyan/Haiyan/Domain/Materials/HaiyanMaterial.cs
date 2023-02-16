using Haiyan.Domain.Calculations;

namespace Haiyan.Domain.Materials
{
    public class HaiyanMaterial
    {
        public string Name { get; set; }
        public List<HaiyanMaterialLayer> Layers { get; set; }
        public Conversion Conversion { get; set; }
    }
}
