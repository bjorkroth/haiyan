using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Lists;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    public class InsulationCategoryIdentifier : ICategoryIdentifier
    {
        public bool CanApply(IIfcProduct product, string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
                return false;

            materialName = materialName.ToUpper();
            if (InsulationMappingList.MappingList.Any(materialName.Contains))
                return true;

            var productName = product.Name?.Value?.ToString()?.ToUpper();

            if (string.IsNullOrEmpty(productName))
                return false;

            if (InsulationMappingList.MappingList.Any(x => productName.Contains(x)))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.Insulation;
    }
}
