using GalaSoft.MvvmLight;
using SwissEphNetTests.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.ViewModels
{
    public class ResultTestViewModel : ViewModelBase
    {
        public class RowData
        {
            public string Name { get; set; }
            public List<CellData> Cells { get; private set; } = new List<CellData>();
        }

        public class CellData
        {
            public RowData Row { get; set; }
            public ResultTestValues Values { get; set; }
            public object Value { get; set; }
        }

        void UpdateModel()
        {
            Rows.Clear();
            if (Result != null)
            {
                var values = Result.TestValues.Values.OrderBy(v => v.Provider.Name).ToList();
                foreach (string name in ValueNames)
                {
                    var row = new RowData { Name = name };
                    foreach (var vals in values)
                    {
                        vals.Values.TryGetValue(name, out object cval);
                        row.Cells.Add(new CellData
                        {
                            Row = row,
                            Values = vals,
                            Value = cval
                        });
                    }
                    Rows.Add(row);
                }
            }
            RaisePropertyChanged(string.Empty);
        }

        public ResultTest Result
        {
            get { return _result; }
            set {
                if (Set(ref _result, value))
                    UpdateModel();
            }
        }
        private ResultTest _result;

        /// <summary>
        /// Test
        /// </summary>
        public ITest Test => Result?.Test;

        /// <summary>
        /// List of values
        /// </summary>
        public Dictionary<string, ResultTestValues> TestValues => Result?.TestValues;

        /// <summary>
        /// List of all values names
        /// </summary>
        public IList<string> ValueNames => Result?.ValueNames.OrderBy(n => n).ToList();

        /// <summary>
        /// The test is a success
        /// </summary>
        public bool Success => Result?.Success ?? false;

        /// <summary>
        /// List of provider names
        /// </summary>
        public IList<string> Providers => Result?.TestValues.Keys.ToList();

        /// <summary>
        /// Rows of data
        /// </summary>
        public ObservableCollection<RowData> Rows { get; private set; } = new ObservableCollection<RowData>();

    }
}
