using Haiyan.Domain.BuildingElements;
using System;
using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class MapToHaiyanCategory
    {
        public static List<HaiyanBuildingElement> Map<T>(List<T> modelObject) where T : IIfcProduct
        {
            var buildingElements = new List<HaiyanBuildingElement>();

            foreach(var item in modelObject)
            {
              
                if (BuildingElementsToIgnore.WillBeIgnored(item))
                {
                    continue;
                }

                var buildingElement = new HaiyanBuildingElement();
                buildingElement.Name = item.Name;
                buildingElement.Description = item.Description;

                buildingElement.IfcGuid = item.GlobalId.ToString();
                buildingElement.GUID = Guid.NewGuid().ToString();
                buildingElement.Type = item.ObjectType.ToString();

                buildingElement.Geometry = null;
                buildingElement.BoverketProductCategory = null;

                buildingElement.Material = MaterialParser.Parse(item);

                buildingElements.Add(buildingElement);
            }

            return buildingElements;
        }
    }
}
