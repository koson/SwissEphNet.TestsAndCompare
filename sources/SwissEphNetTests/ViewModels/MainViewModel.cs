using GalaSoft.MvvmLight;
using SwissEphNetTests.Tests;
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
            Tests = new ObservableCollection<TestViewModel>();
        }

        public async Task LoadTestsAsync()
        {
            IsBusy = true;
            // Init
            var catalog = new AssemblyCatalog(this.GetType().Assembly);
            CompositionContainer container = new CompositionContainer(catalog);
            // Factories
            var factoriesResult = await Task.Run(() =>
            {
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
            foreach (var f in factoriesResult)
                Factories.Add(f);
            // Tests
            var testsResult = await Task.Run(() =>
            {
                List<TestViewModel> tests = new List<TestViewModel>();
                foreach (var test in container.GetExports<ITest>())
                {
                    var vm = new TestViewModel
                    {
                        Test = test.Value,
                        IsSelected = true
                    };
                    tests.Add(vm);
                }
                return tests;
            });
            Tests.Clear();
            foreach (var t in testsResult)
                Tests.Add(t);

            IsBusy = false;
        }

        public ObservableCollection<FactoryViewModel> Factories { get; private set; }

        public ObservableCollection<TestViewModel> Tests { get; private set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { Set(ref _isBusy, value); }
        }
        private bool _isBusy;

    }
}
