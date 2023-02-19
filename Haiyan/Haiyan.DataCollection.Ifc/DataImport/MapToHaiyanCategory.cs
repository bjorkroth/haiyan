using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.DataCollection.Ifc.Extensions;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public static class MapToHaiyanCategory
    {
        public static List<HaiyanBuildingElement> Map<T>(List<T> modelObject, IfcStore model) where T : IIfcProduct
        {
            var buildingElements = new List<HaiyanBuildingElement>();

            foreach(var item in modelObject)
            {
              
                if(item.ShouldBeIgnored()) continue;

                var buildingElement = new HaiyanBuildingElement();
                buildingElement.Name = item.Name;
                buildingElement.Description = item.Description;

                buildingElement.IfcGuid = item.GlobalId.ToString();
                buildingElement.Guid = Guid.NewGuid().ToString();
                buildingElement.Type = item.ObjectType.ToString();

                buildingElement.Geometry = GeometryParser.Parse(item.EntityLabel, model);

                buildingElement.Material = MaterialParser.Parse(item, model);

                buildingElements.Add(buildingElement);
            }

            return buildingElements;
        }
    }
}
