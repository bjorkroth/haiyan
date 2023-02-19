using Haiyan.Domain.Materials;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport.Materials
{
    public interface IMaterialLayerBuilder
    {
        HaiyanMaterialLayer Build(IIfcProduct product, double thickness, string layerName, string layerMaterialName, int layerEntityLabel);
        HaiyanMaterialLayer BuildFromMaterialLayer(IIfcMaterialLayer layer, IIfcProduct product);
    }
}
