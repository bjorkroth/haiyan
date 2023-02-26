using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Geometry;

namespace Haiyan.DataCollection.Ifc.Calculations.Weight
{
    public static class WeightCalculator
    {
        private static double ConcreteDensityPerKilo => 2300;
        private static double SolidWoodDensityPerKilo => 465;
        private static double SteelDensityPerKilo => 2700;
        private static double InsulationDensityPerKilo => 41;
        private static double GlassDensityPerKilo => 2500;

        public static double Calculate(BuildingElementCategory buildingElementCategory, HaiyanGeometry geometry)
        {
            switch (buildingElementCategory)
            {
                case BuildingElementCategory.Concrete:
                    return ConcreteDensityPerKilo * geometry.Volume;
                case BuildingElementCategory.SolidWoods:
                    return SolidWoodDensityPerKilo * geometry.Volume;
                case BuildingElementCategory.SteelAndOtherMetals:
                    return SteelDensityPerKilo * geometry.Volume;
                case BuildingElementCategory.Insulation:
                    return InsulationDensityPerKilo * geometry.Volume;
                case BuildingElementCategory.WindowsDoorsGlass:
                    return GlassDensityPerKilo * geometry.Volume;
                case BuildingElementCategory.Unspecified:
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
