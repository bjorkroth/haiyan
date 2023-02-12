using System.Collections.Generic;
using Xbim.Ifc4.Interfaces;

namespace Haiyan.ConsoleApp.DataImport
{
    public class BuildingElementsToIgnore
    {
        public static bool WillBeIgnored(IIfcProduct product)
        {
            bool ignored = false;

            var listOfIgnoredNames = new List<string>
            {
                "HÅL",
                "DIMPOINT_E",
                "CSTAIRSTEPNUM"
            };

            if (listOfIgnoredNames.Contains(product.Name.ToString().ToUpper()))
            {
                ignored = true;
            }

            return ignored;
        }
    }
}
