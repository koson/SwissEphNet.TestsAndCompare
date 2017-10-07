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
    public class TestSetTopo : BaseTest
    {
        public TestSetTopo()
        {
            Title = "Test set topocentric";
            Description = "Test the planet calculations with a topocentric";
        }

        protected override void RunTest(ResultTestValues result, ISwephProvider provider)
        {
            int iflag = SwephDll.SEFLG_SPEED | SwephDll.SEFLG_TOPOCTR;

            double tjd = provider.SweJulday(1974, 8, 16, 0.5, true);
            double[] geopos = new double[] { 47.853333, 5.333889, 468 };
            double[] x = new double[6];
            string serr = null;

            provider.SweCalcUT(tjd, SwephDll.SE_SUN, iflag, ref x, ref serr);
            result.Values["Topo undefined"] = $"long: {x[0]}\nlat: {x[1]}\ndist: {x[2]}\nspeed long: {x[3]}";

            provider.SweSetTopo(geopos[0], geopos[1], geopos[2]);

            provider.SweCalcUT(tjd, SwephDll.SE_SUN, iflag, ref x, ref serr);
            result.Values["Topo defined"] = $"long: {x[0]}\nlat: {x[1]}\ndist: {x[2]}\nspeed long: {x[3]}";
        }

    }
}
