using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Lists;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class BuildingElementCategoryParser
    {
        public static BuildingElementCategory Parse(IIfcProduct product, string materialName)
        {

            if (!string.IsNullOrEmpty(materialName))
            {
                if (InsulationMappingList.MappingList.Contains(materialName.ToUpper()))
                {
                    return BuildingElementCategory.Insulation;
                }

                if (ConcreteMappingList.MappingList.Contains(materialName.ToString().ToUpper()))
                {
                    return BuildingElementCategory.Concrete;
                }

                if (SteelMappingList.MappingList.Contains(materialName.ToString().ToUpper()))
                {
                    return BuildingElementCategory.SteelAndOtherMetals;
                }

            }

            var returnValue = BuildingElementCategory.Unspecified;

            SteelMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SteelAndOtherMetals;
                    return;
                }
            });

            ConcreteMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Concrete;
                    return;
                }
            });

            InsulationMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Insulation;
                    return;
                }
            });

            WoodMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SolidWoods;
                    return;
                }
            });

            if (materialName == "MISCELLANEOUS/Miscellaneous_Undefined")
            {
                return BuildingElementCategory.Unspecified;
            }

            return returnValue;
        }
    }
}
