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
    public class TestDeltaT : BaseTest
    {
        public TestDeltaT()
        {
            Title = "Test Delta Time";
            Description = "Test deltaT calculations";
        }

        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            var tjd_ut = provider.SweJulday(2017, 8, 26, 11.25, true);
            result.Values["deltaT"] = provider.SweDeltaT(tjd_ut);
        }

    }
}
