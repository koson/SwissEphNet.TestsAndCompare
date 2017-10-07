using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwissEphNetTests.Providers;

namespace SwissEphNetTests.Tests
{
    [Export(typeof(ITest))]
    public class TestVersion : BaseTest
    {
        public TestVersion()
        {
            Title = "Test versions";
            Description = "Test the versions of the libraries";
        }
        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            result.Values["version"] = provider.GetVersion();
        }
    }
}
