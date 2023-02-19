using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public interface IMaterialLayerListBuilder
    {
        IEnumerable<HaiyanMaterialLayer> Build(IIfcProduct product, IList<IIfcRelAssociatesMaterial> productMaterialAssociates);
    }
}
