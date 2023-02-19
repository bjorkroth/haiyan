using Haiyan.DataCollection.Ifc.DataImport;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.Calculations
{
    public static class GetVolume
    {
        public static IIfcValue Get(IIfcProduct product)
        {
            var volume = product.IsDefinedBy
                .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                .OfType<IIfcElementQuantity>()
                .SelectMany(qset => qset.Quantities)
                .OfType<IIfcQuantityVolume>()
                .FirstOrDefault() != null
                ? product.IsDefinedBy
                    .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                    .OfType<IIfcElementQuantity>()
                    .SelectMany(qset => qset.Quantities)
                    .OfType<IIfcQuantityVolume>()
                    .FirstOrDefault()!.VolumeValue
                : 0;

            if (volume != 0)
                return volume;
            return GetProperty.Get(product, "Volume");
        }
    }
}
