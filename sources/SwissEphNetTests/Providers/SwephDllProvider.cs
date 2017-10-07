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

        public string Name => "Swiss Ephemeris DLL";
    }
}
