using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    public class SwephDllProviderFactory : ISwephProviderFactory
    {
        public ISwephProvider CreateProvider() => new SwephDllProvider();
        public string Name => "Swiss Ephemeris DLL";
    }
}
