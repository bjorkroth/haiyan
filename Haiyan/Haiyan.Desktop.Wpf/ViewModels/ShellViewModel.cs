using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using Haiyan.Domain.BuildingElements;
using System.Collections.ObjectModel;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {
            BuildingElements = new ObservableCollection<HaiyanBuildingElement>();

            var modelElements = ModelReader.Read("");
            modelElements.ForEach(element => { BuildingElements.Add(element); });
        }

        private ObservableCollection<HaiyanBuildingElement> _buildingElements;
        public ObservableCollection<HaiyanBuildingElement> BuildingElements { get => _buildingElements; 
            set
            {
                _buildingElements = value;
                NotifyOfPropertyChange(() => BuildingElements);
            }
        }
    }
}
