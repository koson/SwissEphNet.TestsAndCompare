using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    public class SwissEphNetProviderFactory : ISwephProviderFactory
    {
        public ISwephProvider CreateProvider() => new SwissEphNetProvider();
        public string Name => "SwissEph.Net";
    }
}
