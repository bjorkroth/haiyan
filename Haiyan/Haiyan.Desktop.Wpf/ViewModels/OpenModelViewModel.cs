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
