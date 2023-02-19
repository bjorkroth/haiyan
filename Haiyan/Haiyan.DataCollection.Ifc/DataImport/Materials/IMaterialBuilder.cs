using Haiyan.Domain.Materials;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public interface IMaterialBuilder
    {
        HaiyanMaterial Build(IIfcProduct product, IfcStore model);
    }
}
