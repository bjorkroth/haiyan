using Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers;
using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class BuildingElementCategoryIdentifier
    {
        public BuildingElementCategory IdentifyBuildingElementCategory(IIfcProduct product, string materialName)
        {
            var identifiers = new List<ICategoryIdentifier>
            {
                new ConcreteCategoryIdentifier(),
                new GlassCategoryIdentifier(),
                new InsulationCategoryIdentifier(),
                new SteelCategoryIdentifier(),
                new WoodCategoryIdentifier()
            };

            var canApply = identifiers.FirstOrDefault(x => x.CanApply(product, materialName));

            if (canApply == null) return BuildingElementCategory.Unspecified;

            return canApply.Category;
        }
    }
}
