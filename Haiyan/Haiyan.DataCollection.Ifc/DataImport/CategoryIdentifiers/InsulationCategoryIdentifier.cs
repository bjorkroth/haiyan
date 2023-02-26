using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    public class InsulationCategoryIdentifier : ICategoryIdentifier
    {
        private readonly IList<string> _identifiers = new List<string>
        {
            "ISOLERING",
            "MISCELLANEOUS/KOOLTHERM K20",
            "MISCELLANEOUS/EPS 80",
            "EPS",
            "XPS",
            "STENULL",
            "INSULATION",
            "MINERAL WOOL"
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

        public BuildingElementCategory Category => BuildingElementCategory.Insulation;
    }
}
