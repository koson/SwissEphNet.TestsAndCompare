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
        /// The test is a success
        /// </summary>
        public bool Success { get; set; }
    }

}
