using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.DataCollection.Ifc.DataImport
{
    public class BuildingElementsToIgnore
    {
        public static bool WillBeIgnored(IIfcProduct product)
        {
            bool ignored = false;

            var listOfIgnoredNames = new List<string>
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
            };

            if (listOfIgnoredNames.Contains(product.Name.ToString().ToUpper()))
            {
                ignored = true;
            }

            if (product.Name.Value.ToString().Contains("Provision"))
            {
                ignored = true;
            }

            return ignored;
        }
    }
}
