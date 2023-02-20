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
        public IEnumerable<HaiyanBuildingElement> Read(string filePath)
        {
            //filePath = @"C:\Files\Haiyan_IFC\V-57-V-50302030400-QTO.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\V-57-V-70022000.ifc";
            filePath = @"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-70025000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\FS-K-20-V-7000.ifc";
            //filePath = @"C:\Files\Haiyan_IFC\K-20-V-10060000.ifc";

            using var model = IfcStore.Open(filePath);
            var context = new Xbim3DModelContext(model);
            context.CreateContext();

            var categoriesInModel = model.Instances
                .Select(x => x.GetType().ToString())
                .Distinct()
                .ToList();

            var materialLayerBuilder = new MaterialLayerBuilder(model);
            var materialLayerListBuilder = new MaterialLayerListBuilder(materialLayerBuilder);
            var materialBuilder = new MaterialBuilder(materialLayerListBuilder);

            //TODO: Not enumerate all objects to list
            var objects = model.Instances.OfType<IIfcProduct>().ToList();

            if (!objects.Any())
            {
                return Enumerable.Empty<HaiyanBuildingElement>();
            }

            var mapper = new MapToHaiyanCategory(model,materialBuilder);
            return mapper.MapToCategory(objects);
        }
    }
}
