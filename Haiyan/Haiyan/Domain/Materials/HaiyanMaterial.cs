using Haiyan.Domain.Calculations;

namespace Haiyan.Domain.Materials
{
    public class HaiyanMaterial
    {
        public string Name { get; set; }
        public string ResourceId { get; set; }
        public string BoverketResourceCategory { get; set; }
        public Conversion Conversion { get; set; }
    }
}
