using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Tests
{

    /// <summary>
    /// Result of a test
    /// </summary>
    public class ResultTest
    {
        /// <summary>
        /// Test
        /// </summary>
        public ITest Test { get; set; }

        /// <summary>
        /// List of values
        /// </summary>
        public Dictionary<string, ResultTestValues> TestValues { get; private set; } = new Dictionary<string, ResultTestValues>();

        /// <summary>
        /// List of all values names
        /// </summary>
        public IList<string> ValueNames => TestValues.Values.SelectMany(r => r.Values.Keys).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();

        /// <summary>
        /// The test is a success
        /// </summary>
        public bool Success { get; set; }
    }

}
