using Haiyan.DataCollection.Ifc.DataImport;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public static class ModelReader
    {
        public static List<HaiyanBuildingElement> Read(string filePath)
        {
            var list = new List<HaiyanBuildingElement>();

            //filePath = @"C:\Files\Haiyan_IFC\V-57-V-50302030400-QTO.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\V-57-V-70022000.ifc";
            filePath = @"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-70025000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\FS-K-20-V-7000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-10060000.ifc";

            using (var model = IfcStore.Open(filePath))

            {
                var context = new Xbim3DModelContext(model);
                context.CreateContext();

                var categories = model.Instances
                    .Select(x => x.GetType().ToString())
                    .Distinct()
                    .ToList();


                var walls = new ModelInstanceQuery(model).OfType<IIfcWall>();
                var mappedWalls = MapToHaiyanCategory.Map(walls, model);
                list.AddRange(mappedWalls);

                var columns = new ModelInstanceQuery(model).OfType<IIfcColumn>();
                var mappedColumns = MapToHaiyanCategory.Map(columns, model);
                list.AddRange(mappedWalls);

                var slabs = new ModelInstanceQuery(model).OfType<IIfcSlab>();
                var mappedSlabs = MapToHaiyanCategory.Map(slabs, model);
                list.AddRange(mappedSlabs);

                var beams = new ModelInstanceQuery(model).OfType<IIfcBeam>();
                var mappedBeams = MapToHaiyanCategory.Map(beams, model);
                list.AddRange(mappedBeams);

                var roofs = new ModelInstanceQuery(model).OfType<IIfcRoof>();
                var mappedRoofs = MapToHaiyanCategory.Map(roofs, model);
                list.AddRange(mappedRoofs);

                var proxy = new ModelInstanceQuery(model).OfType<IIfcBuildingElementProxy>();
                var mappedProxy = MapToHaiyanCategory.Map(proxy, model);
                list.AddRange(mappedProxy);
            }

            return list;
        }
    }
}
