using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    internal class SteelCategoryIdentifier : ICategoryIdentifier
    {
        private readonly IList<string> _identifiers = new List<string>
        {
            "STEEL",
            "STEEL EXISTING",
            "STEEL/STEEL_UNDEFINED",
            "STEEL/S235JR",
            "HSQ",
            "HEA",
            "HE-A",
            "HEB",
            "VKR",
            "UPE",
            "L-STÅL",
            "GALLERDURK",
            "IPE",
            "KKR",
            "CKR",
            "TRP",
            "PLATE",
            "RR PILE",
            "2 PILE",
            "4-6 PILE",
            "PÅLE",
            "EQUAL L",
            "LL-DOUBLE",
            "STÅL",
            "GÄNGSTÅL",
            "M20",
            "M22",
            "M27",
            "M16"
        };

        public bool CanApply(IIfcProduct product, string materialName)
        {
            if (string.IsNullOrEmpty(materialName))
                return false;

            materialName = materialName.ToUpper();
            if (_identifiers.Any(x => materialName.Contains(x)))
                return true;

            var productName = product.Name?.Value?.ToString()?.ToUpper();

            if (string.IsNullOrEmpty(productName))
                return false;

            if (_identifiers.Any(x => productName.Contains(x)))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.SteelAndOtherMetals;
    }
}
