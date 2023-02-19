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

        public List<HaiyanMaterialLayer> BuildLayers(IIfcMaterialLayerSet ifcMaterialLayerSet, IIfcProduct product)
        {
            if (!ifcMaterialLayerSet.MaterialLayers.Any()) return default;

            var ifcMaterialLayers = ifcMaterialLayerSet.MaterialLayers.ToList();
            var layers = new List<HaiyanMaterialLayer>();

            ifcMaterialLayers.ForEach(layer =>
            {
                layers.Add(_materialLayerBuilder.BuildFromMaterialLayer(layer, product));
            });

            return layers;
        }
    }
}
