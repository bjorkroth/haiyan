using Haiyan.Domain.Calculations;
using Haiyan.Domain.Materials;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public class MaterialBuilder : IMaterialBuilder
    {
        private readonly IMaterialLayerListBuilder _materialLayerListBuilder;

        public MaterialBuilder(IMaterialLayerListBuilder materialLayerListBuilder)
        {
            _materialLayerListBuilder = materialLayerListBuilder;
        }

        public HaiyanMaterial Build(IIfcProduct product, IfcStore model)
        {
            var materialName = "Unknown";
            var material = new HaiyanMaterial(
                materialName,
                new List<HaiyanMaterialLayer>(),
                new Conversion());

            var productMaterialAssociates = product.HasAssociations.OfType<IIfcRelAssociatesMaterial>().ToList();

            if (!productMaterialAssociates.Any())
            {
                material.Layers = _materialLayerListBuilder.Build(product, productMaterialAssociates);
                return material;
            }

            material.Layers = _materialLayerListBuilder.Build(product, productMaterialAssociates);
            
            var ifcMaterial = productMaterialAssociates.FirstOrDefault()?.RelatingMaterial as IIfcMaterial;
            if (ifcMaterial == null) return material;

            material.Name = ifcMaterial.Name.Value.ToString() ?? "Unknown"; 

            return material;
        }
    }
}
