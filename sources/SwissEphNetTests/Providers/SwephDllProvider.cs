using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    public class SwephDllProvider : ISwephProvider
    {
        #region IDisposable Support
        private bool _disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        public string GetVersion()
        {
            return SwephDll.SweVersion();
        }

        public double SweJulday(int year, int month, int day, double hour, bool gregorian)
        {
            return SwephDll.SweJulday(year, month, day, hour, gregorian ? SwephDll.SE_GREG_CAL : SwephDll.SE_JUL_CAL);
        }

        public void SweRevjul(double jd, bool gregorian, ref int year, ref int mon, ref int mday, ref double hour)
        {
            SwephDll.SweRevjul(jd, gregorian ? SwephDll.SE_GREG_CAL : SwephDll.SE_JUL_CAL, ref year, ref mon, ref mday, ref hour);
        }

        public string SweGetPlanetName(int ipl)
        {
            return SwephDll.SweGetPlanetName(ipl);
        }

        public int SweCalcUT(double tjd_ut, int ipl, int iflag, ref double[] xx, ref string serr)
        {
            return SwephDll.SweCalcUT(tjd_ut, ipl, iflag, ref xx, ref serr);
        }

        public string Name => "Swiss Ephemeris DLL";
    }
}
