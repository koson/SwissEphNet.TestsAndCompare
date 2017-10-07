using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{
    /// <summary>
    /// Factory for provider
    /// </summary>
    public interface ISwephProviderFactory
    {
        /// <summary>
        /// Create a new provider
        /// </summary>
        ISwephProvider CreateProvider();

        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }
    }
}
