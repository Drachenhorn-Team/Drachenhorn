using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DSA_Character_Sheet.Dialogs;

namespace DSA_Character_Sheet
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var window = new ExceptionMessageBox(e.Exception, "Im Programm ist ein Fehler aufgetreten.");
            window.Show();
        }
    }
}
