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
        /// Calculate the julian day of a date
        /// </summary>
        double SweJulday(int year, int month, int day, double hour, bool gregorian);

        /// <summary>
        /// Calculate the date of a julian day
        /// </summary>
        void SweRevjul(double jd, bool gregorian, ref int year, ref int mon, ref int mday, ref double hour);

        /// <summary>
        /// Name of the provider
        /// </summary>
        string Name { get; }
    }
}
