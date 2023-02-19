using Haiyan.Domain.Enumerations;
using Haiyan.Domain.Geometry;

namespace Haiyan.ConsoleApp.Calculations
{
    public static class WeightCalculator
    {
        public static double Calculate(BuildingElementCategory buildingElementCategory, HaiyanGeometry geometry)
        {
            switch(buildingElementCategory)
            {
                case BuildingElementCategory.Concrete:
                    return 2300 * geometry.Volume;
                case BuildingElementCategory.SolidWoods:
                    return 465 * geometry.Volume;
                case BuildingElementCategory.SteelAndOtherMetals:
                    return 2700 * geometry.Volume;
                case BuildingElementCategory.Insulation:
                    return 41 * geometry.Volume;
                case BuildingElementCategory.Unspecified: 
                    return 0;
            }

            return 0;
        }
    }
}
