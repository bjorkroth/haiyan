using Haiyan.Domain.BuildingElements;
using System.Collections.Generic;
using System.Linq;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class BuildingElementsWithNoLayerGeometry
    {
        public static List<HaiyanBuildingElement> Get(List<HaiyanBuildingElement> buildingElements)
        {
            return buildingElements
                .Where(x => x.Material.Layers.Any(y => y.LayerGeometry == null))
                .ToList();
        }
    }
}
