using System.Collections.Generic;
using Haiyan.Domain.BuildingElements;

namespace Haiyan.Desktop.Wpf.Events
{
    public class ModelElementsAreRead
    {
        public IEnumerable<HaiyanBuildingElement> ModelElements { get; set; }
    }
}
