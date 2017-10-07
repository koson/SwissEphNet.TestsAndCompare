using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    /// <summary>
    /// Provider of Swiss Ephemeris
    /// </summary>
    public interface ISwephProvider : IDisposable
    {
        /// <summary>
        /// Get the version
        /// </summary>
        string GetVersion();

        /// <summary>
        /// Name of the provider
        /// </summary>
        string Name { get; }
    }
}
