using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Factories = new ObservableCollection<FactoryViewModel>();
        }

        public async Task LoadTestsAsync()
        {
            IsBusy = true;
            var result = await Task.Run(() =>
            {
                var catalog = new AssemblyCatalog(this.GetType().Assembly);
                CompositionContainer container = new CompositionContainer(catalog);
                List<FactoryViewModel> factories = new List<FactoryViewModel>();
                foreach (var factory in container.GetExports<Providers.ISwephProviderFactory>())
                {
                    var vm = new FactoryViewModel
                    {
                        Factory = factory.Value
                    };
                    try
                    {
                        using (var provider = vm.Factory.CreateProvider())
                            vm.Version = provider.GetVersion();
                    }
                    catch (Exception ex)
                    {
                        vm.Version = $"ERR: {ex.Message}";
                    }
                    factories.Add(vm);
                }
                return factories;
            });
            Factories.Clear();
            foreach (var f in result)
                Factories.Add(f);
            IsBusy = false;
        }

        public ObservableCollection<FactoryViewModel> Factories { get; private set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { Set(ref _isBusy, value); }
        }
        private bool _isBusy;

    }
}
