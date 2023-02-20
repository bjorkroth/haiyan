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

        public IEnumerable<HaiyanMaterialLayer> BuildLayers(IIfcMaterialLayerSetUsage ifcMaterialLayerSetUsage, IIfcProduct product)
        {
            if (!ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.Any()) yield break;

            var ifcMaterialLayers = ifcMaterialLayerSetUsage.ForLayerSet.MaterialLayers.ToList();

            foreach (var layer in ifcMaterialLayers)
            {
                yield return _materialLayerBuilder.BuildFromMaterialLayer(layer, product);
            }
        }
    }
}
