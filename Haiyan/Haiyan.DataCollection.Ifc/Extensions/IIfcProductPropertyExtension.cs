using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.Extensions;

public static class IIfcProductPropertyExtension
{
    private const string VolumeName = "Volume";
    public static IIfcValue GetVolume(this IIfcProduct product) => GetIfcPropertyValue(product, VolumeName);

    private static IIfcValue GetIfcPropertyValue(IIfcProduct product, string propertyName)
    {
        var propertySetsWithValues = product.IsDefinedBy
            .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
            .OfType<IIfcPropertySet>()
            .SelectMany(pset => pset.HasProperties);

        var propertiesWithSingleValue = propertySetsWithValues.OfType<IIfcPropertySingleValue>();

        return propertiesWithSingleValue
            .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) ||
                                 p.Name.ToString().ToLower().Contains(propertyName.ToLower()))!.NominalValue;

    }
}