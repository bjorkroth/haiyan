using Haiyan.Domain.BuildingElements;
using Haiyan.Domain.Enumerations;
using System;
using System.Collections.Generic;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class MapToHaiyanCategory
    {
        public static List<HaiyanBuildingElement> Map<T>(List<T> modelObject, IfcStore model) where T : IIfcProduct
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

                buildingElement.Geometry = GeometryParser.Parse(item, model);

                buildingElement.Material = MaterialParser.Parse(item);

                buildingElement.BoverketProductCategory = BuildingElementCategory.Unspecified;

                buildingElements.Add(buildingElement);
            }

            return buildingElements;
        }
    }
}
