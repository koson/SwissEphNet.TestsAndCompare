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
    public class TestJulianDay : BaseTest
    {
        public TestJulianDay()
        {
            Title = "Test Julian day";
            Description = "Test the julian day calculations";
        }

        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            result.Values[$"G: 2017-10-07 14:53"] = provider.SweJulday(2017, 10, 7, new TimeSpan(14, 53, 0).TotalDays, true);
            result.Values[$"J: 2017-10-07 14:53"] = provider.SweJulday(2017, 10, 7, new TimeSpan(14, 53, 0).TotalDays, false);

            result.Values[$"G: Minimal"] = provider.SweJulday(-4713, 11, 24, 12.0, true);
            result.Values[$"J: Minimal"] = provider.SweJulday(-4712, 1, 1, 12.0, false);

            result.Values[$"G: 2000000"] = provider.SweJulday(763, 9, 18, 12.0, true);
            result.Values[$"J: 2000000"] = provider.SweJulday(763, 9, 14, 12.0, false);

            result.Values[$"G: 2000000"] = provider.SweJulday(-1800, 9, 18, 12.0, true);
            result.Values[$"J: 2000000"] = provider.SweJulday(-1800, 9, 14, 12.0, false);

            result.Values[$"G: 1974-08-15 23:30"] = provider.SweJulday(1974, 8, 15, 23 + 30 / 60.0 + 0 / 3600.0, true);
        }

    }
}
