using Caliburn.Micro;
using Haiyan.Domain.BuildingElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {

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
