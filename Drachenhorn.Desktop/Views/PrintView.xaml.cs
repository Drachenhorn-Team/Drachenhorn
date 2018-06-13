using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight.Command;
using mshtml;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für PrintView.xaml
    /// </summary>
    public partial class PrintView : Window, INotifyPropertyChanged
    {

        public PrintView(Func<string> func)
        {
            InitializeComponent();

            this.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "CanPrint")
                    CommandManager.InvalidateRequerySuggested();
            };

            Browser.Navigated += (sender, args) => { CanPrint = true; };
            Task.Run(func).ContinueWith(x => 
                Browser.Dispatcher.Invoke(() => Browser.NavigateToString(x.Result)));
        }

        private bool _canPrint;
        private bool CanPrint
        {
            get => _canPrint;
            set
            {
                if (_canPrint == value)
                    return;
                _canPrint = value;
                OnPropertyChanged();
            }
        }

        private void ExecutePrint(object sender, ExecutedRoutedEventArgs e)
        {
            var doc = Browser.Document as IHTMLDocument2;
            doc?.execCommand("Print", true, null);
        }

        private void PrintCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanPrint;
        }

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}