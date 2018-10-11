using System;
using System.Threading.Tasks;
using System.Windows;
using Drachenhorn.Core.Lang;
using Drachenhorn.Desktop.UI.Dialogs;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.UI.MVVM
{
    public class DialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return ShowMessage(message, title, buttonText, afterHideCallback);
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
            var result = new CommonMessageBox(message, title, buttonText).ShowDialog() == true;

            afterHideCallback?.Invoke();

            return Task.Run(() => result);
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var result = new CommonMessageBox(message, title, buttonConfirmText, buttonCancelText).ShowDialog() == true;

            afterHideCallback?.Invoke(result);

            return Task.Run(() => result);
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