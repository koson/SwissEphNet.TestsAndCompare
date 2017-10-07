using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Tests
{
    /// <summary>
    /// Base of the tests
    /// </summary>
    public abstract class BaseTest : ITest
    {
        /// <summary>
        /// Create a new test
        /// </summary>
        public BaseTest()
        {
            Title = Name;
            Description = "Test " + Name;
        }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name => GetType().Name;

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
