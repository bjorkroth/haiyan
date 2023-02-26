using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Lists;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    internal class SteelCategoryIdentifier : ICategoryIdentifier
    {
        public bool CanApply(IIfcProduct product, string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
                return false;

            materialName = materialName.ToUpper();
            if (SteelMappingList.MappingList.Any(x => materialName.Contains(x)))
                return true;

            var productName = product.Name?.Value?.ToString()?.ToUpper();

            if (string.IsNullOrEmpty(productName))
                return false;

            if (SteelMappingList.MappingList.Any(x => productName.Contains(x)))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.SteelAndOtherMetals;
    }
}
