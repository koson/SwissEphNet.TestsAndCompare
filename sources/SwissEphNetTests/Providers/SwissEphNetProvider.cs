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

        public string Name => "SwissEph.Net";

        protected SwissEph Sweph { get { CheckDisposed(); return _sweph; } }
    }
}
