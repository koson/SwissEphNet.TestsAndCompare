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

        public string Name => "Swiss Ephemeris DLL";
    }
}
