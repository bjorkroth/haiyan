using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Haiyan.Desktop.Wpf.Events;
using Haiyan.Desktop.Wpf.Views;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<StatusMessageEvent>, IHandle<ModelElementsAreRead>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator, IModelReader modelReader)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);

            MaterialDataView = new MaterialDataView();
            StatusField = "";

            MainContentView = new OpenModelView
            {
                DataContext = new OpenModelViewModel(_eventAggregator, modelReader)
            };
        }

        private UserControl _mainContentView { get; set; }

        public UserControl MainContentView
        {
            get => _mainContentView;
            set
            {
                _mainContentView = value;
                NotifyOfPropertyChange(() => MainContentView);
            }
        }

        public UserControl MaterialDataView { get; set; }

        private string _statusField { get; set; }

        public string StatusField
        {
            get => _statusField;
            set
            {
                _statusField = value;
                NotifyOfPropertyChange(() => StatusField);
            }
        }


        public Task HandleAsync(StatusMessageEvent message, CancellationToken cancellationToken)
        {
            StatusField = message.Message;
            return Task.CompletedTask;
        }

        public Task HandleAsync(ModelElementsAreRead message, CancellationToken cancellationToken)
        {
            MaterialDataView = new MaterialDataView
            {
                DataContext = new MaterialDataViewModel(message.ModelElements)
            };

            MainContentView = MaterialDataView;

            return Task.CompletedTask;
        }
    }
}
