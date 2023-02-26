using Haiyan.DataCollection.Ifc.DataImport;
using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;

namespace Haiyan.DataCollection.Ifc.ModelReaders
{
    public class ModelReader : IModelReader, IDisposable
    {
        private readonly IList<IDisposable> _disposableObjects;

        public ModelReader()
        {
            _disposableObjects = new List<IDisposable>();
        }

        public IEnumerable<HaiyanBuildingElement> Read(string filePath)
        {
            var model = IfcStore.Open(filePath);

            _disposableObjects.Add(model);
        
            var context = new Xbim3DModelContext(model);
            context.CreateContext();

            var categoriesInModel = model.Instances
                .Select(x => x.GetType().ToString())
                .Distinct()
                .ToList();

            var materialLayerBuilder = new MaterialLayerBuilder(model);
            var materialLayerListBuilder = new MaterialLayerListBuilder(materialLayerBuilder);
            var materialBuilder = new MaterialBuilder(materialLayerListBuilder);
            var mapper = new MapToHaiyanCategory(materialBuilder);

            //TODO: Not enumerate all objects to list
            var objects = model.Instances.OfType<IIfcProduct>().ToList();

            if (!objects.Any())
            {
                return Enumerable.Empty<HaiyanBuildingElement>();
            }

            return mapper.MapToCategory(model, objects);
        }

        public void Dispose()
        {
            foreach (var disposableObject in _disposableObjects)
            {
                disposableObject.Dispose();
            }
        }
    }
}
