using Haiyan.DataCollection.Ifc.DataImport;
using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public class ModelReader : IModelReader
    {
        public List<HaiyanBuildingElement> Read(string filePath)
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

                var categoriesInModel = model.Instances
                    .Select(x => x.GetType().ToString())
                    .Distinct()
                    .ToList();

                var materialLayerBuilder = new MaterialLayerBuilder(model);
                var materialLayerListBuilder = new MaterialLayerListBuilder(materialLayerBuilder, model);
                var materialBuilder = new MaterialBuilder(materialLayerListBuilder);
                var mapper = new MapToHaiyanCategory(materialBuilder);

                var walls = new ModelInstanceQuery(model).OfType<IIfcWall>();
                var mappedWalls = mapper.Map(walls, model);
                list.AddRange(mappedWalls);

                var columns = new ModelInstanceQuery(model).OfType<IIfcColumn>();
                var mappedColumns = mapper.Map(columns, model);
                list.AddRange(mappedColumns);

                var slabs = new ModelInstanceQuery(model).OfType<IIfcSlab>();
                var mappedSlabs = mapper.Map(slabs, model);
                list.AddRange(mappedSlabs);

                var beams = new ModelInstanceQuery(model).OfType<IIfcBeam>();
                var mappedBeams = mapper.Map(beams, model);
                list.AddRange(mappedBeams);

                var roofs = new ModelInstanceQuery(model).OfType<IIfcRoof>();
                var mappedRoofs = mapper.Map(roofs, model);
                list.AddRange(mappedRoofs);

                var proxy = new ModelInstanceQuery(model).OfType<IIfcBuildingElementProxy>();
                var mappedProxy = mapper.Map(proxy, model);
                list.AddRange(mappedProxy);
            }

            return list;
        }
    }
}
