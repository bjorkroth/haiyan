using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.DataCollection.Ifc.DataImport.ModelElementMapper;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class MapToHaiyanCategory
    {
        private readonly IMaterialBuilder _materialBuilder;

        public MapToHaiyanCategory(IMaterialBuilder materialBuilder)
        {
            _materialBuilder = materialBuilder;
        }

        public IEnumerable<HaiyanBuildingElement> MapToCategory(IfcStore model, IEnumerable<IIfcProduct> modelObjects)
        {
            var modelElementMapper = new List<IModelElementMapper>
            {
                new WallModelElementMapper(model, _materialBuilder),
                new SlabModelElementMapper(model, _materialBuilder),
                new BeamModelElementMapper(model, _materialBuilder),
                new ColumnModelElementMapper(model, _materialBuilder),
                new RoofModelElementMapper(model, _materialBuilder),
                new ProxyModelElementMapper(model, _materialBuilder),
                new DoorModelElementMapper(model, _materialBuilder),
                new WindowModelElementMapper(model, _materialBuilder),
            };

            foreach (var modelObject in modelObjects)
            {
                var modelElementMapperForObject = modelElementMapper.FirstOrDefault(x => x.CanApply(modelObject));

                if (modelElementMapperForObject == null)
                    continue;

                yield return modelElementMapperForObject.MapBuildingElement(modelObject);
            }
        }
    }
}
