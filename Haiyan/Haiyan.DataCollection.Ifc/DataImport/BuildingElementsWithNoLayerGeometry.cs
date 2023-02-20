using Haiyan.Domain.BuildingElements;

namespace Haiyan.DataCollection.Ifc.DataImport
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
