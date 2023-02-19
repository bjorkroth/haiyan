using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class BuildMaterialLayersFromMaterialList
    {
        private readonly IMaterialLayerBuilder _materialLayerBuilder;

        public BuildMaterialLayersFromMaterialList(IMaterialLayerBuilder materialLayerBuilder)
        {
            _materialLayerBuilder = materialLayerBuilder;
        }

        public List<HaiyanMaterialLayer> BuildLayers(IIfcMaterialList ifcMaterialList, IIfcProduct product)
        {
            if (!ifcMaterialList.Materials.Any())
            {
                return default;
            }

            var layers = new List<HaiyanMaterialLayer>();

            foreach (var material in ifcMaterialList.Materials)
            {
                layers.Add(_materialLayerBuilder.Build(product, 0, material.Name, material.Name, material.EntityLabel));
            }

            return layers;
        }
    }
}
