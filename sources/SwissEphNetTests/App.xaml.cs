using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SwissEphNetTests
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application, Services.IShellService
    {
        public static ViewModels.ModelLocator Locator { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Locator = Resources["Locator"] as ViewModels.ModelLocator;
        }

        public string SelectedWriteFile(string title, string filters, string defaultExt)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                CheckPathExists = true,
                Title = title,
                AddExtension = true,
                CheckFileExists = false,
                CreatePrompt = false,
                OverwritePrompt=true
            };
            if (!string.IsNullOrWhiteSpace(filters))
            {
                dialog.Filter = filters;
            }
            if (!string.IsNullOrWhiteSpace(defaultExt))
            {
                dialog.DefaultExt = defaultExt;
            }
            if (dialog.ShowDialog(MainWindow) == true)
            {
                return dialog.FileName;
            }
            return null;
        }

    }
}
