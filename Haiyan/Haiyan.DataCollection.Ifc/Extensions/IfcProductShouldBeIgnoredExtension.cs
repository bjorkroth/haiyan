using System.Collections.ObjectModel;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.Extensions
{
    public static class IfcProductShouldBeIgnoredExtension
    {
        private static readonly IEnumerable<string> IgnoredTypes = new ReadOnlyCollection<string>(new List<string>()
        {
            "VOID",
            "HÅL",
            "HÅL K",
            "DIMPOINT_E",
            "CSTAIRSTEPNUM",
            "CSTAIRNUM_1",
            "CSTAIRNUM_2",
            "CSTAIRNUM_3",
            "CSTAIRNUM_4",
            "CSTAIRNUM_5",
            "CSTAIRNUM_6",
            "CSTAIRNUM_7",
            "CSTAIRNUM_8",
            "CSTAIRNUM_9",
            "CSTAIRNUM_10",
            "PBP",
            "SURVEYMARKER_PBP",
            "PROJECT BASE POINT",
            "NV PROJECT BASE POINT"
        });

        public static bool ShouldBeIgnored(this IIfcProduct product)
        {
            if (!product.Name.HasValue)
            {
                return false;
            }

            if (IgnoredTypes.Contains(product.Name.Value.ToString().ToUpper()))
            {
                return true;
            }

            if (product.Name.Value.ToString().Contains("Provision"))
            {
                return true;
            }

            return false;
        }
    }
}
