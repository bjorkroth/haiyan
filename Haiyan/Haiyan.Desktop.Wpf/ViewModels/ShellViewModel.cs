using System;
using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using System.Collections.ObjectModel;
using Haiyan.Desktop.Wpf.ViewModelFactory;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IDisposable
    {
        public ShellViewModel(IModelReader modelReader)
        {
            MaterialLayers = new ObservableCollection<MaterialLayerViewModel>();

            //var filePath = @"C:\Files\Haiyan_IFC\V-57-V-50302030400-QTO.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\V-57-V-70022000.ifc";
            var filePath = @"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70025000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70022000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\FS-K-20-V-7000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-10060000.ifc";

            try
            {
                var modelElements = modelReader.Read(filePath);
                MaterialLayers = new MaterialLayerViewModelFactory().Create(modelElements);
            }
            finally
            {
                modelReader.Dispose();
            }
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

        public void Dispose()
        {
        }
    }
}
