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
        /// Run the test with a provider
        /// </summary>
        protected abstract void RunTest(ResultTestValues result, Providers.ISwephProvider provider);

        /// <summary>
        /// Run the test
        /// </summary>
        public virtual ResultTest RunTest(IEnumerable<Providers.ISwephProvider> providers)
        {
            var result = new ResultTest { Test = this };
            // Run the test
            foreach (var provider in providers)
            {
                var values = new ResultTestValues { Provider = provider };
                try
                {
                    RunTest(values, provider);
                }
                catch (Exception ex) { values.Error = ex; result.Success = false; }
                result.TestValues[provider.Name] = values;
            }

            return result;
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
