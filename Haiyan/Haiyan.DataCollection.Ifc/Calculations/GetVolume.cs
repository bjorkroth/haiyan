using Haiyan.DataCollection.Ifc.Extensions;
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
                .FirstOrDefault()!.VolumeValue;


            if (volume.Value != null)
                return volume;

            return product.GetVolume();
        }
    }
}
