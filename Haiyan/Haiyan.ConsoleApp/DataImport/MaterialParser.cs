using Haiyan.Domain.Materials;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class MaterialParser
    {
        public static HaiyanMaterial Parse(IIfcProduct product)
        {
            var material = new HaiyanMaterial();

            var materials = product.HasAssociations.OfType<IIfcRelAssociatesMaterial>();

            if (!materials.Any()) return material;

            var firstMaterial = materials.FirstOrDefault();
            var relatedMaterial = firstMaterial.RelatingMaterial as IIfcMaterial;

            material.Name = relatedMaterial.Name;

            return material;
        }
    }       
}
