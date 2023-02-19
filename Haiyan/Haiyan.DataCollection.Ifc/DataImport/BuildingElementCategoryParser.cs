using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Lists;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public static class BuildingElementCategoryParser
    {
        public static BuildingElementCategory Parse(IIfcProduct product, string materialName)
        {
            //TODO: _BuildingElementCatergoryParsers.FirstOrDefault(x => x.CanParse(product, materialName).Catory)
            if (!string.IsNullOrEmpty(materialName))
            {
                if (InsulationMappingList.MappingList.Contains(materialName.ToUpper()))
                {
                    return BuildingElementCategory.Insulation;
                }

                if (ConcreteMappingList.MappingList.Contains(materialName.ToUpper()))
                {
                    return BuildingElementCategory.Concrete;
                }

                if (SteelMappingList.MappingList.Contains(materialName.ToUpper()))
                {
                    return BuildingElementCategory.SteelAndOtherMetals;
                }

                if (GlassMappingList.MappingList.Contains(materialName.ToUpper()))
                {
                    return BuildingElementCategory.WindowsDoorsGlass;
                }

            }

            var returnValue = BuildingElementCategory.Unspecified;

            if (product.Name == null)
            {
                return returnValue;
            }

            SteelMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SteelAndOtherMetals;
                }
            });

            ConcreteMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Concrete;
                }
            });

            InsulationMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Insulation;
                }
            });

            WoodMappingList.MappingList.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SolidWoods;
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
