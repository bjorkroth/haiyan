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

        public IEnumerable<HaiyanMaterialLayer> BuildLayers(IIfcMaterialList ifcMaterialList, IIfcProduct product)
        {
            if (!ifcMaterialList.Materials.Any())
                yield break;

            foreach (var material in ifcMaterialList.Materials)
            {
                yield return _materialLayerBuilder.Build(product, 0, material.Name, material.Name, material.EntityLabel);
            }
        }
    }
}
