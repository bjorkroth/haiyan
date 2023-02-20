using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class MaterialLayerListBuilder : IMaterialLayerListBuilder
    {
        private readonly IMaterialLayerBuilder _materialLayerBuilder;

        public MaterialLayerListBuilder(IMaterialLayerBuilder materialLayerBuilder)
        {
            _materialLayerBuilder = materialLayerBuilder;
        }

        public IEnumerable<HaiyanMaterialLayer> Build(IIfcProduct product, IList<IIfcRelAssociatesMaterial> productMaterialAssociates)
        {
            if (!productMaterialAssociates.Any())
            {
                var layerCreatedFromProduct = _materialLayerBuilder.Build(product, 0, "", "", product.EntityLabel);
                return new List<HaiyanMaterialLayer> { layerCreatedFromProduct };
            }

            var layers = new List<HaiyanMaterialLayer>();
            var firstMaterial = productMaterialAssociates.FirstOrDefault();

            if (firstMaterial == null) return layers;

            var relatedMaterial = firstMaterial.RelatingMaterial as IIfcMaterial;

            if (firstMaterial.RelatingMaterial is IIfcMaterialLayerSetUsage materialSetLayerUsage)
            {
                //TODO: enumerable
                layers = new BuildMaterialLayersFromMaterialLayerSetUsage(_materialLayerBuilder)
                    .BuildLayers(materialSetLayerUsage, product).ToList();
            }
            else if (firstMaterial.RelatingMaterial is IIfcMaterialLayerSet materialSetLayer)
            {
                //TODO: enumerable
                layers = new BuildMaterialLayersFromMaterialLayerSet(_materialLayerBuilder)
                    .BuildLayers(materialSetLayer, product).ToList();
            }
            else if (firstMaterial.RelatingMaterial is IIfcMaterialList materialList)
            {
                //TODO: enumerable
                layers = new BuildMaterialLayersFromMaterialList(_materialLayerBuilder)
                    .BuildLayers(materialList, product).ToList();
            }

            if (relatedMaterial == null)
            {
                return layers;
            }

            if (layers.Any()) 
                return layers;

            var layerByMaterial = _materialLayerBuilder.Build(product, 0, relatedMaterial.Name, relatedMaterial.Name, product.EntityLabel);
            layers = new List<HaiyanMaterialLayer> { layerByMaterial };

            return layers;
        }
    }
}
