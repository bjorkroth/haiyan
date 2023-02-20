using Haiyan.DataCollection.Ifc.DataImport.Materials;
using Haiyan.DataCollection.Ifc.DataImport.ModelElementMapper;
using Haiyan.Domain.BuildingElements;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class MapToHaiyanCategory
    {
        private readonly IfcStore _model;
        private readonly IMaterialBuilder _materialBuilder;

        public MapToHaiyanCategory(IfcStore model, IMaterialBuilder materialBuilder)
        {
            _model = model;
            _materialBuilder = materialBuilder;
        }

        public IEnumerable<HaiyanBuildingElement> MapToCategory(IEnumerable<IIfcProduct> modelObjects)
        {
            var modelElementMapper = new List<IModelElementMapper>
            {
                new WallModelElementMapper(_model, _materialBuilder),
                new SlabModelElementMapper(_model, _materialBuilder),
                new BeamModelElementMapper(_model, _materialBuilder),
                new ColumnModelElementMapper(_model, _materialBuilder),
                new RoofModelElementMapper(_model, _materialBuilder),
                new ProxyModelElementMapper(_model, _materialBuilder),
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
