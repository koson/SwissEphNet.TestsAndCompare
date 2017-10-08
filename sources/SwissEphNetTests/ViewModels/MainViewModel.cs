using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SwissEphNetTests.Services;
using SwissEphNetTests.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private TestEngine _engine;
        private IShellService _shell;

        public MainViewModel(TestEngine engine, IShellService shell)
        {
            _engine = engine;
            _shell = shell;
            Factories = new ObservableCollection<FactoryViewModel>();
            Tests = new ObservableCollection<TestViewModel>();
            TestResults = new ObservableCollection<ResultTestViewModel>();

            RunTestsCommand = new RelayCommand(
                async () => await RunTestsAsync(),
                () => !IsBusy
                );

            ExportToCsvCommand = new RelayCommand(
                async () => await ExportResultsAsync(ExportFormat.CSV),
                () => TestResults.Count > 0
                );
            ExportToMinimalCsvCommand = new RelayCommand(
                async () => await ExportResultsAsync(ExportFormat.MinimalCSV),
                () => TestResults.Count > 0
                );
        }

        public async Task LoadTestsAsync()
        {
            IsBusy = true;
            // Factories
            var factoriesResult = await Task.Run(() =>
            {
                List<FactoryViewModel> factories = new List<FactoryViewModel>();
                foreach (var factory in _engine.GetProviderFactories())
                {
                    var vm = new FactoryViewModel
                    {
                        Factory = factory
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
                foreach (var test in _engine.GetTests())
                {
                    var vm = new TestViewModel
                    {
                        Test = test,
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
                    foreach (var result in _engine.RunTests(providers, tests))
                    {
                        progress.Report(new ResultTestViewModel { Result = result });
                    }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExportResultsAsync(ExportFormat format)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var fileName = _shell.SelectedWriteFile("Select the exported file", "CSV files (*.csv)|*.csv|All files (*.*)|*.*", ".csv");
                if (string.IsNullOrWhiteSpace(fileName)) return;
                var results = TestResults.Select(r => r.Result).ToList();
                using (var stream = System.IO.File.Create(fileName))
                {
                    await Task.Run(() =>
                    {
                        _engine.ExportTests(results, stream, format);
                    });
                }
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
                    ExportToCsvCommand.RaiseCanExecuteChanged();
                    ExportToMinimalCsvCommand.RaiseCanExecuteChanged();
                }
            }
        }
        private bool _isBusy;

        public RelayCommand RunTestsCommand { get; private set; }
        public RelayCommand ExportToCsvCommand { get; private set; }
        public RelayCommand ExportToMinimalCsvCommand { get; private set; }
    }
}
