using Haiyan.Domain.BuildingElements;
using Haiyan.Domain.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Haiyan.ConsoleApp.Calculations
{
    public static class SumWeightByBuildingElementCategory
    {
        public static double Sum(List<HaiyanBuildingElement> buildingElements, BuildingElementCategory category)
        {
            if(!buildingElements.Any()) return 0;

            var weightForCategory = buildingElements
                  .Select(x => x.Material.Layers.Where(y => y.BoverketProductCategory == category))
                  .SelectMany(x => x)
                  .Where(x => x.LayerGeometry != null)
                  .Select(x => x.LayerGeometry.Weight);

            return weightForCategory.Sum();
        }
    }
}
