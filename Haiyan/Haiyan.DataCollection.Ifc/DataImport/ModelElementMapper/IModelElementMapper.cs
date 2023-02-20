using Haiyan.Domain.BuildingElements;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.ModelElementMapper
{
    public interface IModelElementMapper
    {
        bool CanApply(IIfcProduct product);
        HaiyanBuildingElement MapBuildingElement(IIfcProduct product);
    }
}
