using Caliburn.Micro;
using Haiyan.DataCollection.Ifc.ModelReaders;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Haiyan.Desktop.Wpf.Events;
using Haiyan.Desktop.Wpf.Views;

namespace Haiyan.Desktop.Wpf.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<StatusMessageEvent>, IHandle<ModelElementsAreRead>, IHandle<OpenAnotherModelEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IModelReader _modelReader;

        public ShellViewModel(IEventAggregator eventAggregator, IModelReader modelReader)
        {
            _eventAggregator = eventAggregator;
            _modelReader = modelReader;
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

        private UserControl _materialDataView { get; set; }

        public UserControl MaterialDataView
        {
            get => _materialDataView;
            set
            {
                _materialDataView = value;
                NotifyOfPropertyChange(() => MaterialDataView);
            }
        }

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
                DataContext = new MaterialDataViewModel(_eventAggregator, message.ModelElements)
            };

            MainContentView = MaterialDataView;

            return Task.CompletedTask;
        }

        public Task HandleAsync(OpenAnotherModelEvent message, CancellationToken cancellationToken)
        {
            MainContentView = new OpenModelView
            {
                DataContext = new OpenModelViewModel(_eventAggregator, _modelReader)
            };

            return Task.CompletedTask;
        }
    }
}
