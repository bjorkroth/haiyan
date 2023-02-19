using Haiyan.Domain.Calculations;

namespace Haiyan.Domain.Materials
{
    public record HaiyanMaterial
    {
        public string Name { get; set; }
        public IEnumerable<HaiyanMaterialLayer> Layers { get; set; }
        public Conversion Conversion { get; set; }
    }
}
