using GalaSoft.MvvmLight;
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
        public MainViewModel()
        {
            Factories = new ObservableCollection<Providers.ISwephProviderFactory>
            {
                new Providers.SwephDllProviderFactory(),
                new Providers.SwissEphNetProviderFactory()
            };
            Versions = new ObservableCollection<Tuple<string, string>>();
        }

        public void LoadVersions()
        {
            foreach (var fact in Factories)
            {
                try
                {
                    using (var provider = fact.CreateProvider())
                        Versions.Add(Tuple.Create(provider.Name, provider.GetVersion()));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public ObservableCollection<Providers.ISwephProviderFactory> Factories { get; private set; }
        public ObservableCollection<Tuple<string, string>> Versions { get; private set; }
    }
}
