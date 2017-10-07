using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Tests
{
    /// <summary>
    /// Values of a test
    /// </summary>
    public class ResultTestValues
    {
        /// <summary>
        /// Provider
        /// </summary>
        public Providers.ISwephProvider Provider { get; set; }

        /// <summary>
        /// Liste of the values
        /// </summary>
        public Dictionary<string, object> Values { get; private set; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Error if failed
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Delay of the test
        /// </summary>
        public TimeSpan TestDelay { get; set; }
    }
}
