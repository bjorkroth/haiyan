using Haiyan.Domain.BuildingElements;
using Haiyan.Domain.Materials;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class MaterialLayerViewModel
    {
        public MaterialLayerViewModel(HaiyanBuildingElement buildingElement, HaiyanMaterialLayer materialLayer)
        {
            BuildingElementCategory = buildingElement.GetType().Name;
            BuildingElementName = buildingElement.Name;
            BuildingElementType = buildingElement.Type;
            BoverketProductCategory = materialLayer.BoverketProductCategory.ToString();
            MaterialThickness = materialLayer.Thickness;
            MaterialVolume = materialLayer.LayerGeometry.Volume;
            MaterialWeight = materialLayer.LayerGeometry.Weight;
        }

        public string BuildingElementCategory { get; set; }
        public string BuildingElementName { get; set; }
        public string BuildingElementType { get; set; }
        public string BoverketProductCategory { get; set; }
        public double MaterialThickness { get; set; }
        public double MaterialWeight { get; set; }
        public double MaterialVolume { get; set; }
    }
}
