using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Materials;
using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class BuildingElementCategoryParser
    {
        public static BuildingElementCategory Parse(IIfcProduct product, HaiyanMaterial material)
        {
            var listOfInsulation = new List<string>
            {
                "ISOLERING",
                "MISCELLANEOUS/KOOLTHERM K20",
                "MISCELLANEOUS/EPS 80",
                "EPS",
                "XPS",
                "STENULL",
                "INSULATION"
            };

            var listOfConcrete = new List<string>
            {
                "DEFAULT CONCRETE",
                "CAST-IN-PLACE",
                "PRECAST",
                "CONCRETE",
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
                "RD",
                "RD/F",
                "RD-PLATTOR",
                "TT",
                "TT/F",
                "UNDERGJUTNING",
                "PÅGJUTNING",
                "FOUNDATION",
            };

            var listOfSteel = new List<string>
            {
                "STEEL/STEEL_UNDEFINED",
                "STEEL/S235JR",
                "HSQ",
                "HEA",
                "HE-A",
                "HEB",
                "VKR",
                "UPE",
                "L-STÅL",
                "GALLERDURK"
            };

            var listOfWood = new List<string>()
            {
                "TRÄ",
                "TIMBER"
            };

            if (material != null && material?.Name != null)
            {
                
                if (listOfInsulation.Contains(material.Name.ToUpper()))
                {
                    return BuildingElementCategory.Insulation;
                }

                if (listOfConcrete.Contains(material.Name.ToString().ToUpper()))
                {
                    return BuildingElementCategory.Concrete;
                }

                if (listOfSteel.Contains(material.Name.ToString().ToUpper()))
                {
                    return BuildingElementCategory.SteelAndOtherMetals;
                }

            }

            var returnValue = BuildingElementCategory.Unspecified;

            listOfSteel.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SteelAndOtherMetals;
                    return;
                }
            });

            listOfConcrete.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Concrete;
                    return;
                }
            });

            listOfInsulation.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.Insulation;
                    return;
                }
            });

            listOfWood.ForEach(x =>
            {
                if (product.Name.Value.ToString().ToUpper().Contains(x))
                {
                    returnValue = BuildingElementCategory.SolidWoods;
                    return;
                }
            });

            if (material.Name == "MISCELLANEOUS/Miscellaneous_Undefined")
            {
                return BuildingElementCategory.Unspecified;
            }

            return returnValue;
        }
    }
}
