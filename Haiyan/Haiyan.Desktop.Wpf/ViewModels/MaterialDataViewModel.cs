using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haiyan.Desktop.Wpf.Events;
using Haiyan.Desktop.Wpf.ViewModelFactory;
using Haiyan.Domain.BuildingElements;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class MaterialDataViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public MaterialDataViewModel(IEventAggregator eventAggregator, IEnumerable<HaiyanBuildingElement> modelElements)
        {
            _eventAggregator = eventAggregator;
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

        public async Task OpenModel()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new OpenAnotherModelEvent());
        }
    }
}
