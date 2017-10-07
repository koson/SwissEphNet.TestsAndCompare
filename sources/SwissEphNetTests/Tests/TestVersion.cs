using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Tests
{
    [Export(typeof(ITest))]
    public class TestVersion : BaseTest
    {
        public TestVersion()
        {
            Title = "Test versions";
            Description = "Test the versions of the libraries";
        }
    }
}
