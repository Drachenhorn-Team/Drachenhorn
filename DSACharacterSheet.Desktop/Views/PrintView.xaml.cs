using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für PrintView.xaml
    /// </summary>
    public partial class PrintView : Window, INotifyPropertyChanged
    {
        private bool _canPrint = false;

        private bool CanPrint
        {
            get { return _canPrint; }
            set
            {
                if (_canPrint == value)
                    return;
                _canPrint = value;
                OnPropertyChanged();
            }
        }

        public PrintView(string html)
        {
            InitializeComponent();

            Browser.Navigated += (sender, args) => { CanPrint = true; };
            Browser.NavigateToString(html);
        }

        private void ExecutePrint(object sender, ExecutedRoutedEventArgs e)
        {
            var doc = Browser.Document as mshtml.IHTMLDocument2;
            doc?.execCommand("Print", true, null);
        }


        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}
