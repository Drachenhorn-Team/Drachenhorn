using Drachenhorn.Core.Lang;
using Drachenhorn.Desktop.UI.Dialogs;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Drachenhorn.Desktop.UI.MVVM
{
    public class DialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => { new ExceptionMessageBox(error, title).ShowDialog(); });
        }

        public async Task ShowMessage(string message, string title)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(
                    LanguageManager.TextTranslate(message),
                    LanguageManager.TextTranslate(title),
                    MessageBoxButton.OK);
            });
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var result = new CommonMessageBox(message, title, buttonConfirmText, buttonCancelText).ShowDialog() == true ? true : false;

            afterHideCallback?.Invoke(result);

            return Task.Run(() =>
            {
                return result;
            });
        }

        public async Task ShowMessageBox(string message, string title)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(
                    LanguageManager.TextTranslate(message),
                    LanguageManager.TextTranslate(title),
                    MessageBoxButton.OK);
            });
        }
    }
}