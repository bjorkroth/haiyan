using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using Haiyan.Domain.BuildingElements;
using Haiyan.Domain.Materials;
using System.Collections.ObjectModel;
using System.Linq;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {
            BuildingElements = new ObservableCollection<HaiyanBuildingElement>();
            MaterialLayers = new ObservableCollection<HaiyanMaterialLayer>();

            var modelElements = ModelReader.Read("");
            modelElements.ForEach(element => { BuildingElements.Add(element); });

            var layers = modelElements
                .Select(x => x.Material.Layers)
                .SelectMany(x => x)
                .ToList();

            layers.ForEach(layer => { MaterialLayers.Add(layer); });

        }

        private ObservableCollection<HaiyanBuildingElement> _buildingElements;
        public ObservableCollection<HaiyanBuildingElement> BuildingElements { 
            get => _buildingElements; 
            set
            {
                _buildingElements = value;
                NotifyOfPropertyChange(() => BuildingElements);
            }
        }


        private ObservableCollection<HaiyanMaterialLayer> _materialLayers;
        public ObservableCollection<HaiyanMaterialLayer> MaterialLayers
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
