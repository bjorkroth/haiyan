using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class BuildMaterialLayersFromMaterialLayerSet
    {
        private readonly IMaterialLayerBuilder _materialLayerBuilder;

        public BuildMaterialLayersFromMaterialLayerSet(IMaterialLayerBuilder materialLayerBuilder)
        {
            _materialLayerBuilder = materialLayerBuilder;
        }

        public IEnumerable<HaiyanMaterialLayer> BuildLayers(IIfcMaterialLayerSet ifcMaterialLayerSet, IIfcProduct product)
        {
            if (!ifcMaterialLayerSet.MaterialLayers.Any())
                yield break;

            var ifcMaterialLayers = ifcMaterialLayerSet.MaterialLayers.ToList();

            foreach (var layer in ifcMaterialLayers)
            {
                yield return _materialLayerBuilder.BuildFromMaterialLayer(layer, product);
            }
        }
    }
}
