using SwissEphNetTests.Providers;
using SwissEphNetTests.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
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

        static string CsvEncode(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            if (value.IndexOfAny(new[] { '"', '\n', '\r', ',', ';' }) >= 0)
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }

        private static void ExportToCSV(IEnumerable<ResultTest> tests, List<string> providers, StreamWriter writer)
        {
            // Write header
            writer.Write("test,title,success,value-name,");
            for (int p = 0; p < providers.Count; p++)
                writer.Write($"p{p + 1}-name,p{p + 1}-value,p{p + 1}-delay,p{p + 1}-status,");
            writer.WriteLine("value-status");
            // Write result
            foreach (var test in tests)
            {
                string testfields = $"{CsvEncode(test.Test.Name)},{CsvEncode(test.Test.Title)},{(test.Success ? "true" : "false")},";
                string valueStatus = "success";
                foreach (string valueName in test.ValueNames)
                {
                    writer.Write(testfields);
                    writer.Write(CsvEncode(valueName));
                    writer.Write(",");
                    for (int p = 0; p < providers.Count; p++)
                    {
                        if (test.TestValues.TryGetValue(providers[p], out ResultTestValues values))
                        {
                            values.Values.TryGetValue(valueName, out object valValue);
                            string valValueStr = valValue?.ToString();
                            if (valValue is DateTime dt) valValueStr = dt.ToString("o");
                            else if (valValue is DateTimeOffset dto) valValueStr = dto.ToString("o");
                            else if (valValue is float flt) valValueStr = flt.ToString(CultureInfo.InvariantCulture);
                            else if (valValue is double dbl) valValueStr = dbl.ToString(CultureInfo.InvariantCulture);
                            else if (valValue is decimal dec) valValueStr = dec.ToString(CultureInfo.InvariantCulture);
                            string valStatus = "success";
                            if (values.Error != null)
                            {
                                valStatus = $"#err:{values.Error.Message}";
                                valueStatus = "failed";
                            }
                            writer.Write($"{CsvEncode(providers[p])},{CsvEncode(valValueStr)},{CsvEncode(values.TestDelay.ToString())},{CsvEncode(valStatus)},");
                        }
                        else
                        {
                            writer.Write($"{providers[p]},,,none,");
                        }
                    }
                    writer.WriteLine(valueStatus);
                }
            }
        }
        private static void ExportToMinimalCSV(IEnumerable<ResultTest> tests, List<string> providers, StreamWriter writer)
        {
            // Write header
            writer.Write("test,title,success,value-name,");
            for (int p = 0; p < providers.Count; p++)
                writer.Write($"{CsvEncode(providers[p])},");
            writer.WriteLine("value-status");
            // Write result
            foreach (var test in tests)
            {
                string testfields = $"{CsvEncode(test.Test.Name)},{CsvEncode(test.Test.Title)},{(test.Success ? "true" : "false")},";
                string valueStatus = "success";
                foreach (string valueName in test.ValueNames)
                {
                    writer.Write(testfields);
                    writer.Write(CsvEncode(valueName));
                    writer.Write(",");
                    for (int p = 0; p < providers.Count; p++)
                    {
                        if (test.TestValues.TryGetValue(providers[p], out ResultTestValues values))
                        {
                            values.Values.TryGetValue(valueName, out object valValue);
                            string valValueStr = valValue?.ToString();
                            if (valValue is DateTime dt) valValueStr = dt.ToString("o");
                            else if (valValue is DateTimeOffset dto) valValueStr = dto.ToString("o");
                            else if (valValue is float flt) valValueStr = flt.ToString(CultureInfo.InvariantCulture);
                            else if (valValue is double dbl) valValueStr = dbl.ToString(CultureInfo.InvariantCulture);
                            else if (valValue is decimal dec) valValueStr = dec.ToString(CultureInfo.InvariantCulture);
                            if (values.Error != null)
                            {
                                valValueStr = $"#err:{values.Error.Message}";
                                valueStatus = "failed";
                            }
                            writer.Write($"{CsvEncode(valValueStr)},");
                        }
                        else
                        {
                            writer.Write($",");
                        }
                    }
                    writer.WriteLine(valueStatus);
                }
            }
        }

        /// <summary>
        /// Export the result of tests in a stream
        /// </summary>
        public void ExportTests(IEnumerable<ResultTest> tests, Stream destination, ExportFormat format)
        {
            var providers = tests.SelectMany(t => t.TestValues.Keys).Distinct().ToList();
            using (var writer = new StreamWriter(destination, Encoding.UTF8, 4096, true))
            {
                switch (format)
                {
                    case ExportFormat.MinimalCSV:
                        ExportToMinimalCSV(tests, providers, writer);
                        break;
                    case ExportFormat.CSV:
                    default:
                        ExportToCSV(tests, providers, writer);
                        break;
                }
            }
        }

    }

    public enum ExportFormat
    {
        CSV,
        MinimalCSV
    }

}
