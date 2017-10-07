using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{

    /// <summary>
    /// Sweph Dll interop
    /// </summary>
    public static class SwephDll
    {
        /// <summary>
        /// DLL Name
        /// </summary>
        const String SwephDllName = "swedll-2.06.dll";

        /// <summary>
        /// Preload the DLL
        /// </summary>
        static SwephDll()
        {
            // Check the size of the pointer
            String folder = IntPtr.Size == 8 ? "x64" : "x86";
            // Build the full library file name
            String libraryFile = Path.Combine(Path.GetDirectoryName(typeof(SwephDll).Assembly.Location), "libraries", folder, SwephDllName);
            // Load the library
            var res = LoadLibrary(libraryFile);
            if (res == IntPtr.Zero)
                throw new InvalidOperationException("Failed to load the library.");
        }

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = false)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        /// <summary>
        /// Get the library version
        /// </summary>
        /// <returns>Number represents version</returns>
        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_version")]
        private extern static IntPtr _swe_version(StringBuilder buff);
        public static string SweVersion()
        {
            StringBuilder buffer = new StringBuilder(64);
            _swe_version(buffer);
            return buffer.ToString();
        }
    }

}
