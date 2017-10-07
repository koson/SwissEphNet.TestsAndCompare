using GalaSoft.MvvmLight;
using SwissEphNetTests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.ViewModels
{
    public class TestViewModel : ViewModelBase
    {

        public ITest Test { get; set; }
        public string Name => Test?.Name;
        public string Title => Test?.Title;
        public string Description => Test?.Description;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(ref _isSelected, value); }
        }
        private bool _isSelected;

    }
}
