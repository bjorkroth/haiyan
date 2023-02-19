using Haiyan.Domain.Materials;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class MaterialLayerListBuilder : IMaterialLayerListBuilder
    {
        private readonly IMaterialLayerBuilder _materialLayerBuilder;
        private readonly IfcStore _model;

        public MaterialLayerListBuilder(IMaterialLayerBuilder materialLayerBuilder, IfcStore model)
        {
            _materialLayerBuilder = materialLayerBuilder;
            _model = model;
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
            var materialSetLayerUsage = firstMaterial.RelatingMaterial as IIfcMaterialLayerSetUsage;
            var materialSetLayer = firstMaterial.RelatingMaterial as IIfcMaterialLayerSet;
            var materialList = firstMaterial.RelatingMaterial as IIfcMaterialList;

            if (materialSetLayerUsage != null)
            {
                layers = new BuildMaterialLayersFromMaterialLayerSetUsage(_materialLayerBuilder)
                    .BuildLayers(materialSetLayerUsage, product);
            }
            else if (materialSetLayer != null)
            {
                layers = new BuildMaterialLayersFromMaterialLayerSet(_materialLayerBuilder)
                    .BuildLayers(materialSetLayer, product);
            }
            else if (materialList != null)
            {
                layers = new BuildMaterialLayersFromMaterialList(_materialLayerBuilder)
                    .BuildLayers(materialList, product);
            }

            if (relatedMaterial == null)
            {
                return layers;
            }

            if (!layers.Any())
            {
                var layerByMaterial = _materialLayerBuilder.Build(product, 0, relatedMaterial.Name, relatedMaterial.Name, product.EntityLabel);
                layers = new List<HaiyanMaterialLayer> { layerByMaterial };
            }

            return layers;
        }
    }
}
