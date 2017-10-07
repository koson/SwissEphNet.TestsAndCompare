using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    public class SwissEphNetProvider : ISwephProvider
    {
        private SwissEph _sweph;

        public SwissEphNetProvider()
        {
            _sweph = new SwissEph();
        }

        #region IDisposable Support
        private bool _disposedValue = false;
        protected void CheckDisposed()
        {
            if (_disposedValue)
                throw new ObjectDisposedException(nameof(SwissEphNetProvider));
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _sweph.Dispose();
                    _sweph = null;
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
            return Sweph.swe_version();
        }

        public double SweJulday(int year, int month, int day, double hour, bool gregorian)
        {
            return Sweph.swe_julday(year, month, day, hour, gregorian ? SwissEph.SE_GREG_CAL : SwissEph.SE_JUL_CAL);
        }

        public void SweRevjul(double jd, bool gregorian, ref int year, ref int mon, ref int mday, ref double hour)
        {
            Sweph.swe_revjul(jd, gregorian ? SwissEph.SE_GREG_CAL : SwissEph.SE_JUL_CAL, ref year, ref mon, ref mday, ref hour);
        }

        public double SweDeltaT(double tjd) => Sweph.swe_deltat(tjd);

        public string SweGetPlanetName(int ipl)
        {
            return Sweph.swe_get_planet_name(ipl);
        }

        public int SweCalcUT(double tjd_ut, int ipl, int iflag, ref double[] xx, ref string serr)
        {
            xx = new double[32];
            return Sweph.swe_calc_ut(tjd_ut, ipl, iflag, xx, ref serr);
        }

        public int SweCalc(double tjd, int ipl, int iflag, ref double[] xx, ref string serr)
        {
            xx = new double[32];
            return Sweph.swe_calc(tjd, ipl, iflag, xx, ref serr);
        }

        public string Name => "SwissEph.Net";

        protected SwissEph Sweph { get { CheckDisposed(); return _sweph; } }
    }
}
