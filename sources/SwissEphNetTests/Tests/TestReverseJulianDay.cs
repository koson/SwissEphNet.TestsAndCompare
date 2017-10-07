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
    public class TestReverseJulianDay : BaseTest
    {
        public TestReverseJulianDay()
        {
            Title = "Test Reverse Julian day";
            Description = "Test the date calculations from a julian day";
        }

        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            int y = 0, m = 0, d = 0; double ut = 0;

            provider.SweRevjul(0, true, ref y, ref m, ref d, ref ut);
            result.Values["G: 0"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";

            provider.SweRevjul(0, false, ref y, ref m, ref d, ref ut);
            result.Values["J: 0"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";

            provider.SweRevjul(2000000, true, ref y, ref m, ref d, ref ut);
            result.Values["G: 2000000"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";
            provider.SweRevjul(2000000, false, ref y, ref m, ref d, ref ut);
            result.Values["J: 2000000"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";

            provider.SweRevjul(2456774.20375, true, ref y, ref m, ref d, ref ut);
            result.Values["G: 2456774.20375"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";

            provider.SweRevjul(2442275.47916667, true, ref y, ref m, ref d, ref ut);
            result.Values["G: 2442275.47916667"] = $"y:{y} - m:{m} - d:{d} - ut:{ut}";
        }

    }
}
