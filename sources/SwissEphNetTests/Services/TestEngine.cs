using SwissEphNetTests.Providers;
using SwissEphNetTests.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Services
{
    public class TestEngine
    {
        CompositionContainer _container;

        CompositionContainer BuildContainer()
        {
            var catalog = new AssemblyCatalog(this.GetType().Assembly);
            return new CompositionContainer(catalog);
        }

        CompositionContainer GetContainer() => _container ?? (_container = BuildContainer());

        /// <summary>
        /// Get the provider factories list
        /// </summary>
        public IEnumerable<ISwephProviderFactory> GetProviderFactories()
        {
            return GetContainer().GetExports<ISwephProviderFactory>().Select(f => f.Value);
        }

        /// <summary>
        /// Get the tests list
        /// </summary>
        public IEnumerable<ITest> GetTests()
        {
            return GetContainer().GetExports<ITest>().Select(f => f.Value);
        }

        /// <summary>
        /// Run tests
        /// </summary>
        public IEnumerable<ResultTest> RunTests(IEnumerable<ISwephProvider> providers = null, IEnumerable<ITest> tests = null, bool disposeProviders = true)
        {
            try
            {
                // Extract datas
                providers = (providers ?? GetProviderFactories().Select(f => f.CreateProvider())).ToList();
                tests = (tests ?? GetTests()).ToList();
                // Run the tests
                foreach (var test in tests)
                {
                    yield return test.RunTest(providers);
                }
            }
            finally
            {
                // Free the providers
                if (disposeProviders)
                {
                    foreach (var provider in providers)
                        provider.Dispose();
                }
            }
        }

    }
}
