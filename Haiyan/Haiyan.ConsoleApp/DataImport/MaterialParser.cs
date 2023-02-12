using Haiyan.Domain.Materials;
using System;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class MaterialParser
    {
        public static HaiyanMaterial Parse(IIfcProduct product)
        {
            var material = new HaiyanMaterial();

            var ifcRelAssociatesMaterials = product.HasAssociations.OfType<IIfcRelAssociatesMaterial>();

            if (!ifcRelAssociatesMaterials.Any()) return material;

            var firstMaterial = ifcRelAssociatesMaterials.FirstOrDefault();

            if(ifcRelAssociatesMaterials.Count() > 1)
            {
                Console.WriteLine("Multiple materials for item " + product.Name.ToString());
            }

            var relatedMaterial = firstMaterial.RelatingMaterial as IIfcMaterial;

            material.Name = relatedMaterial.Name;

            return material;
        }
    }       
}
