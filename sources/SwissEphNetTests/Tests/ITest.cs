using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Tests
{
    /// <summary>
    /// Test
    /// </summary>
    public interface ITest
    {

        /// <summary>
        /// Run the test
        /// </summary>
        ResultTest RunTest(IEnumerable<Providers.ISwephProvider> providers);

        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }
    }
}
