using Haiyan.Domain.Calculations;

namespace Haiyan.Domain.Materials
{
    public class HaiyanMaterial
    {
        public HaiyanMaterial(string name, IEnumerable<HaiyanMaterialLayer> layers, Conversion conversion)
        {
            Name = name;
            Layers = layers;
            Conversion = conversion;
        }

        public string Name { get; set; }
        public IEnumerable<HaiyanMaterialLayer> Layers { get; set; }
        public Conversion Conversion { get; set; }
    };
}
