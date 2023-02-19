using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.DataCollection.Ifc.Extensions;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class MapToHaiyanCategory
    {
        private IMaterialBuilder _materialBuilder;

        public MapToHaiyanCategory(IMaterialBuilder materialBuilder)
        {
            _materialBuilder = materialBuilder;
        }

        public List<HaiyanBuildingElement> Map<T>(List<T> modelObject, IfcStore model) where T : IIfcProduct
        {
            var buildingElements = new List<HaiyanBuildingElement>();

            foreach(var item in modelObject)
            {
                if(item.ShouldBeIgnored()) continue;

                var buildingElement = new HaiyanBuildingElement
                {
                    Name = item.Name ?? "",
                    Description = item.Description ?? "",
                    IfcGuid = item.GlobalId.ToString(),
                    Guid = Guid.NewGuid().ToString(),
                    Type = item.ObjectType.ToString() ?? "",
                    Geometry = GeometryParser.Parse(item.EntityLabel, model),
                    Material = _materialBuilder.Build(item, model)
                };

                buildingElements.Add(buildingElement);
            }

            return buildingElements;
        }
    }
}
