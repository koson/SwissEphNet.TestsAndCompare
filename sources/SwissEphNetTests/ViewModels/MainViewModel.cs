using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            TestResults = new ObservableCollection<ResultTestViewModel>();

            RunTestsCommand = new RelayCommand(
                async () => await RunTestsAsync(),
                () => !IsBusy
                );
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

        public async Task RunTestsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var tests = Tests.Where(t => t.IsSelected).Select(t => t.Test).ToList();
                var providers = Factories.Select(f => f.Factory.CreateProvider()).ToList();
                TestResults.Clear();
                IProgress<ResultTestViewModel> progress = new Progress<ResultTestViewModel>(r => TestResults.Add(r));
                await Task.Run(() =>
                {
                    foreach (var test in tests)
                    {
                        var result = test.RunTest(providers);
                        progress.Report(new ResultTestViewModel { Result = result });
                    }
                });
                foreach (var provider in providers)
                    provider.Dispose();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<FactoryViewModel> Factories { get; private set; }

        public ObservableCollection<TestViewModel> Tests { get; private set; }

        public ObservableCollection<ResultTestViewModel> TestResults { get; private set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (Set(ref _isBusy, value))
                {
                    RunTestsCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private bool _isBusy;

        public RelayCommand RunTestsCommand { get; private set; }

    }
}
