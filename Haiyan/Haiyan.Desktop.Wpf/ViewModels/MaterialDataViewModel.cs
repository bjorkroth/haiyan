using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Haiyan.Desktop.Wpf.ViewModelFactory;
using Haiyan.Domain.BuildingElements;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class MaterialDataViewModel : Screen
    {
        public MaterialDataViewModel(IEnumerable<HaiyanBuildingElement> modelElements)
        {
            MaterialLayers = new ObservableCollection<MaterialLayerViewModel>();
            MaterialLayers = new MaterialLayerViewModelFactory().Create(modelElements);
        }

        private ObservableCollection<MaterialLayerViewModel> _materialLayers;
        public ObservableCollection<MaterialLayerViewModel> MaterialLayers
        {
            get => _materialLayers;
            set
            {
                _materialLayers = value;
                NotifyOfPropertyChange(() => MaterialLayers);
            }
        }
    }
}
