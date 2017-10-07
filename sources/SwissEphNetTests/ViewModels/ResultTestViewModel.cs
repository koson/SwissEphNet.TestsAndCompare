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
            public bool IsFailed => Cells.Any(c => c.IsFailed);
        }

        public class CellData
        {
            public RowData Row { get; set; }
            public ResultTestValues Values { get; set; }
            public object Value { get; set; }
            public int CompareErrors { get; set; }
            public bool IsFailed => CompareErrors > 0;
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
                    for (int i = 0, cnt = values.Count; i < cnt; i++)
                    {
                        var vals = values[i];
                        vals.Values.TryGetValue(name, out object cval);
                        var cell = new CellData
                        {
                            Row = row,
                            Values = vals,
                            Value = cval
                        };
                        row.Cells.Add(cell);
                        for (int j = 0; j < i; j++)
                        {
                            values[j].Values.TryGetValue(name, out object tval);
                            if (!object.Equals(cval, tval))
                                cell.CompareErrors++;
                        }
                    }
                    Rows.Add(row);
                    if (row.IsFailed)
                        Result.Success = false;
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
        public IList<ResultTestValues> TestValues => Result?.TestValues.Values.OrderBy(v => v.Provider.Name).ToList();

        /// <summary>
        /// List of all values names
        /// </summary>
        public IList<string> ValueNames => Result?.ValueNames.OrderBy(n => n).ToList();

        /// <summary>
        /// The test is a success
        /// </summary>
        public bool Success => Result?.Success ?? false;

        /// <summary>
        /// Rows of data
        /// </summary>
        public ObservableCollection<RowData> Rows { get; private set; } = new ObservableCollection<RowData>();

    }
}
