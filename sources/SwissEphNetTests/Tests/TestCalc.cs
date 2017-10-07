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
    public class TestCalc : BaseTest
    {
        public TestCalc()
        {
            Title = "Test planet calculation 'swe_calc()'";
            Description = "Test the planet calculations from a julian day";
        }

        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            int iflag = SwephDll.SEFLG_SPEED;
            var tjd_ut = provider.SweJulday(2017, 10, 7, new TimeSpan(16, 31, 0).TotalDays, true);
            var tjd_et = tjd_ut + provider.SweDeltaT(tjd_ut);
            for (int p = SwephDll.SE_SUN; p <= SwephDll.SE_CHIRON; p++)
            {
                if (p == SwephDll.SE_EARTH) continue;
                double[] x2 = new double[6];
                string serr = null;
                var iflgret = provider.SweCalc(tjd_et, p, iflag, ref x2, ref serr);
                string sname = provider.SweGetPlanetName(p);
                result.Values[sname] = $"long: {x2[0]}\nlat: {x2[1]}\ndist: {x2[2]}\nspeed long: {x2[3]}";
            }
        }

    }
}
