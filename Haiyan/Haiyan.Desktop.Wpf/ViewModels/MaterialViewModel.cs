using Haiyan.Domain.BuildingElements;
using Haiyan.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class MaterialViewModel
    {
        public MaterialViewModel(HaiyanBuildingElement buildingElement, HaiyanMaterialLayer materialLayer)
        {

        }

        public string BuildingElementName { get; set; }
        public string BoverketProductCategory { get; set; }
        public double MaterialThickness { get; set; }
        public double MaterialWeight { get; set; }
        public double MaterialVolume { get; set; }
    }
}
