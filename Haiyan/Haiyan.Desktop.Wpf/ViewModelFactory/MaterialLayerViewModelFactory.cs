using System.Collections.Generic;
using System.Collections.ObjectModel;
using Haiyan.Desktop.Wpf.ViewModels;
using Haiyan.Domain.BuildingElements;

namespace Haiyan.Desktop.Wpf.ViewModelFactory
{
    public class MaterialLayerViewModelFactory
    {
        public ObservableCollection<MaterialLayerViewModel> Create(IEnumerable<HaiyanBuildingElement> buildingElements)
        {
            var materialLayers = new ObservableCollection<MaterialLayerViewModel>();

            foreach (var buildingElement in buildingElements)
            {
                foreach (var materialLayer in buildingElement.Material.Layers)
                {
                    materialLayers.Add(new MaterialLayerViewModel(buildingElement, materialLayer));
                }
            }

            return materialLayers;
        }
    }
}
