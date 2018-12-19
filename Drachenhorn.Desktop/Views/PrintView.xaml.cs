using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Xml.Sheet;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    ///     Interaktionslogik für PrintView.xaml
    /// </summary>
    public partial class PrintView
    {
        #region c'tor

        public PrintView(CharacterSheet sheet)
        {
            InitializeComponent();

            if (!(DataContext is PrintViewModel)) return;

            var model = (PrintViewModel) DataContext;

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

        #endregion

        private void TemplateSelectionChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != "SelectedTemplate" || !(DataContext is PrintViewModel))
                return;

            var model = (PrintViewModel) DataContext;

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
            if (!(DataContext is PrintViewModel))
                return;

            var model = (PrintViewModel) DataContext;


            Task.Run(() =>
            {
                try
                {
                    model.IsLoading = true;
                    return generateFunc();
                }
                catch (InvalidOperationException e)
                {
                    var errorHtml =
                        "<!DOCTYPE html><html xmlns =\"http://www.w3.org/1999/xhtml \"><body><h1>Error in PrintTemplate! " +
                        e.Message + "</h1>";

                    if (e.InnerException is AggregateException)
                        foreach (var exception in (e.InnerException as AggregateException)?.InnerExceptions)
                            errorHtml += "<p>" + exception.Message + "</p>";

                    return errorHtml + "</body></html>";
                }
            }).ContinueWith(x =>
            {
                Browser.Dispatcher.Invoke(() => Browser.NavigateToString(x.Result));
                model.IsLoading = false;
            });
        }


        private void ExecutePrint(object sender, ExecutedRoutedEventArgs e)
        {
            //var doc = Browser.Document as IHTMLDocument2;
            //doc?.execCommand("Print", true, null);
        }

        private void PrintCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!(DataContext is PrintViewModel)) return;

            e.CanExecute = ((PrintViewModel) DataContext).CanPrint;
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