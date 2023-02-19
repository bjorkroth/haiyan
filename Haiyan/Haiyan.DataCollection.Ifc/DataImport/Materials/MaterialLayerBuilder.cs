using Haiyan.DataCollection.Ifc.Calculations.Weight;
using Haiyan.Domain.Materials;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class MaterialLayerBuilder : IMaterialLayerBuilder
    {
        private readonly IfcStore _model;

        public MaterialLayerBuilder(IfcStore model)
        {
            _model = model;
        }

        public HaiyanMaterialLayer Build(IIfcProduct product, double thickness, string layerName, string layerMaterialName, int layerEntityLabel)
        {
            if (string.IsNullOrEmpty(layerMaterialName))
            {
                layerMaterialName = product.Name?.Value.ToString() ?? "";
            }

            return CreateLayer(product, thickness, layerName, layerMaterialName, layerEntityLabel);
        }

        public HaiyanMaterialLayer BuildFromMaterialLayer(IIfcMaterialLayer layer, IIfcProduct product)
        {
            return CreateLayer(product, layer.LayerThickness, layer.Name ?? "", layer.Material.Name, layer.EntityLabel);
        }

        private HaiyanMaterialLayer CreateLayer(IIfcProduct product, double thickness, string layerName, string layerMaterialName, int layerEntityLabel)
        {
            var materialLayer = new HaiyanMaterialLayer
            {
                Name = layerMaterialName,
                Thickness = thickness,
                BoverketProductCategory = BuildingElementCategoryParser.Parse(product, layerMaterialName)
            };

            try
            {
                materialLayer.LayerGeometry = GeometryParser.Parse(layerEntityLabel, _model);
                materialLayer.LayerGeometry.Weight = WeightCalculator.Calculate(materialLayer.BoverketProductCategory, materialLayer.LayerGeometry);
            }
            catch
            {
                Console.WriteLine("Could not calculate volume and weight for " + layerName);
            }

            return materialLayer;
        }
    }
}
