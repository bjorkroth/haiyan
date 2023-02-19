using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class BuildMaterialLayersFromMaterialLayerSetUsage
    {
        private readonly IMaterialLayerBuilder _materialLayerBuilder;

        public BuildMaterialLayersFromMaterialLayerSetUsage(IMaterialLayerBuilder materialLayerBuilder)
        {
            _materialLayerBuilder = materialLayerBuilder;
        }

        public List<HaiyanMaterialLayer> BuildLayers(IIfcMaterialLayerSetUsage ifcMaterialLayerSetUsage, IIfcProduct product)
        {
            if (!ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.Any()) return default;

            var ifcMaterialLayers = ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.ToList();
            var layers = new List<HaiyanMaterialLayer>();

            ifcMaterialLayers.ForEach(layer =>
            {
                layers.Add(_materialLayerBuilder.BuildFromMaterialLayer(layer, product));
            });

            return layers;

        }
    }
}
