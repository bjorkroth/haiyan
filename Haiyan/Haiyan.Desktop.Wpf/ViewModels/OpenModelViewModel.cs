using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using Haiyan.Desktop.Wpf.Events;
using Microsoft.Win32;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class OpenModelViewModel : Screen, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModelReader _modelReader;

        public OpenModelViewModel(IEventAggregator eventAggregator, IModelReader modelReader)
        {
            _eventAggregator = eventAggregator;
            _modelReader = modelReader;
        }

        public async Task OpenModel()
        {
            //var filePath = @"C:\Files\Haiyan_IFC\V-57-V-50302030400-QTO.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\V-57-V-70022000.ifc";
            var filePath = @"C:\Files\Haiyan_IFC\KP-23-V-70022000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70025000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70022000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-70098000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\FS-K-20-V-7000.ifc";
            //var filePath = @"C:\Files\Haiyan_IFC\K-20-V-10060000.ifc";

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IFC files|*.ifc;*.ifcZIP;*.zip";

            var dialogResult = openFileDialog.ShowDialog();

            if(dialogResult == null)
                return;

            if (!dialogResult.Value) 
                return;

            try
            {
                var modelElements = _modelReader.Read(openFileDialog.FileName);

                await _eventAggregator.PublishOnUIThreadAsync(new ModelElementsAreRead
                {
                    ModelElements = modelElements
                });
            }
            finally
            {
                _modelReader.Dispose();
            }
        }

        public void Dispose()
        {
        }
    }
}
