using System;
using System.Linq;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public static class GetProperty
    {
        public static IIfcValue Get(IIfcProduct product, string name)
        {
            var propertySetsWithValues = product.IsDefinedBy
                .SelectMany(r => r.RelatingPropertyDefinition.PropertySetDefinitions)
                .OfType<IIfcPropertySet>()
                .SelectMany(pset => pset.HasProperties);

            var propertiesWithSingleValue = propertySetsWithValues.OfType<IIfcPropertySingleValue>();

            return propertiesWithSingleValue
                .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase) ||
                                     p.Name.ToString().ToLower().Contains(name.ToLower()))!.NominalValue;
        }
    }
}
