using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.ViewModels
{
    public class FactoryViewModel
    {
        public Providers.ISwephProviderFactory Factory { get; set; }
        public string Name => Factory.Name;
        public string Version { get; set; }
    }
}
