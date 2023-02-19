using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class ModelInstanceQuery
    {
        private IfcStore _model;

        public ModelInstanceQuery(IfcStore model) {
            _model = model;
        }

        public List<T> OfType<T>() where T : IIfcProduct
        {
            var objects = _model.Instances.OfType<T>().ToList();

            return objects;
        }
    }
}
