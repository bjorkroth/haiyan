using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.SharedBldgElements;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    public class ConcreteCategoryIdentifier : ICategoryIdentifier
    {
        private readonly IList<string> _identifiers = new List<string>
        {
            "CONCRETE EXISTING",
            "CONCRETE - EXISTING",
            "DEFAULT CONCRETE",
            "CAST-IN-PLACE",
            "PRECAST",
            "PREFAB",
            "CONCRETE",
            "LÄTTBETONG",
            "BETONG",
            "BTG",
            "CONCRETE/CONCRETE_UNDEFINED",
            "CONCRETE/C30/37",
            "CONCRETE/C40/50",
            "C20/25",
            "C25/30",
            "C28/35",
            "C30/37",
            "C32/40",
            "C35/45",
            "C40/50",
            "C45/55",
            "C50/60",
            "C55/67",
            "C60/75",
            "RD/F",
            "RD-PLATTOR",
            "TT/F",
            "HD/F",
            "HDF",
            "UNDERGJUTNING",
            "PÅGJUTNING",
            "FOUNDATION",
            "FUNDATION",
            "BEVEL",
            "SLAB EDGE",
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

            var productType = product.GetType().Name;
            if (productName == "W" && productType == nameof(IfcWall))
                return true;
            if (productName == "V" && productType == nameof(IfcWall))
                return true;

            return false;
        }

        public BuildingElementCategory Category => BuildingElementCategory.Concrete;
    }
}
