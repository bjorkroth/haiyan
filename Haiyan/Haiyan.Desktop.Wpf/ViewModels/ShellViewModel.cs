using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using System.Collections.ObjectModel;
using Haiyan.Desktop.Wpf.ViewModelFactory;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel(IModelReader modelReader)
        {
            MaterialLayers = new ObservableCollection<MaterialLayerViewModel>();

            var modelElements = modelReader.Read("");
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
