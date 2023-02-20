using Caliburn.Micro;
using Haiyan.Desktop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using Haiyan.DataCollection.Ifc.ModelReaders;

namespace Haiyan.Desktop.Wpf
{
    public class HaiyanDesktopBootstrapperBase : BootstrapperBase
    {
        SimpleContainer container;

        public HaiyanDesktopBootstrapperBase()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IModelReader, ModelReader>();

            container.PerRequest<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
