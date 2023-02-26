using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    internal class GlassCategoryIdentifier : ICategoryIdentifier
    {
        private readonly IList<string> _identifiers = new List<string>
        {
            "MISCELLANEOUS/Glas",
            "GLASPARTI",
            "GLAS"
        };

        public bool CanApply(IIfcProduct product, string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
                return false;

            materialName = materialName.ToUpper();
            if (_identifiers.Any(materialName.Contains))
                return true;

            var productName = product.Name?.Value?.ToString()?.ToUpper();

            if (string.IsNullOrEmpty(productName))
                return false;

            if (_identifiers.Any(x => productName.Contains(x)))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.WindowsDoorsGlass;
    }
}
