using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Services
{
    public interface IShellService
    {
        string SelectedWriteFile(string title, string filters, string defaultExt);
    }
}
