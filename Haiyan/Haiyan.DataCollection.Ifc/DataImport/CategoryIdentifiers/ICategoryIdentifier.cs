using Haiyan.Domain.Enumerations;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.CategoryIdentifiers
{
    public interface ICategoryIdentifier
    {
        bool CanApply(IIfcProduct product, string materialName);
        BuildingElementCategory Category { get; }
    }
}
