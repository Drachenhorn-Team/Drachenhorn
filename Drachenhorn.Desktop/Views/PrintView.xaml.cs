using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight.Command;
using mshtml;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für PrintView.xaml
    /// </summary>
    public partial class PrintView
    {
        public PrintView(CharacterSheet sheet)
        {
            InitializeComponent();

            if (!(this.DataContext is PrintViewModel)) return;

            var model = (PrintViewModel) this.DataContext;

            model.CurrentSheet = sheet;
            model.PropertyChanged += TemplateSelectionChanged;

            Browser.Navigated += (sender, args) =>
            {
                model.CanPrint = true;
                CommandManager.InvalidateRequerySuggested();
            };

            UseOwn.Checked += (s, a) => TemplateSelectionChanged(s, new PropertyChangedEventArgs("SelectedTemplate"));
            UseOwn.Unchecked += (s, a) => TemplateSelectionChanged(s, new PropertyChangedEventArgs("SelectedTemplate"));

            TemplateSelectionChanged(this, new PropertyChangedEventArgs("SelectedTemplate"));
        }

        private void TemplateSelectionChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "SelectedTemplate" || !(this.DataContext is PrintViewModel))
                return;

            var model = (PrintViewModel) this.DataContext;

            if (UseOwn.IsChecked == true && model.SelectedTemplate == null)
            {
                if (model.SelectedTemplate == null) return;

                PrintToBrowser(() => PrintingManager.GenerateHtml(model.CurrentSheet, model.SelectedTemplate));
            }
            else
            {
                PrintToBrowser(() => PrintingManager.GenerateHtml(model.CurrentSheet));
            }
        }

        private void PrintToBrowser(Func<string> generateFunc)
        {
            if (!(this.DataContext is PrintViewModel))
                return;

            var model = (PrintViewModel)this.DataContext;


            Task.Run(() =>
            {
                model.IsLoading = true;
                return generateFunc();
            }).ContinueWith(x =>
            {
                Browser.Dispatcher.Invoke(() => Browser.NavigateToString(x.Result));
                model.IsLoading = false;
            });
        }


        private void ExecutePrint(object sender, ExecutedRoutedEventArgs e)
        {
            var doc = Browser.Document as IHTMLDocument2;
            doc?.execCommand("Print", true, null);
        }

        private void PrintCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!(this.DataContext is PrintViewModel)) return;

            e.CanExecute = ((PrintViewModel)this.DataContext).CanPrint;
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