using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.UI;
using Drachenhorn.Desktop.UI.Dialogs;

namespace Drachenhorn.Desktop.UI.MVVM
{
    public class DialogService : IDialogService
    {
        /// <inheritdoc />
        public Task<int> ShowMessage(string message, string title = null,
            IEnumerable<string> buttons = null, Action<int> afterHideCallback = null)
        {
            //TODO: MetroMessageDialog
            return ShowMessageExternal(message, title, buttons, afterHideCallback);
        }

        /// <inheritdoc />
        public Task<int> ShowMessageExternal(string message, string title = null,
            IEnumerable<string> buttons = null, Action<int> afterHideCallback = null)
        {
            if (buttons == null || !buttons.Any())
                buttons = new List<string> {LanguageManager.Translate("UI.OK")};

            var result = new CommonMessageBox(message, title, buttons.ToArray()).ShowDialog();

            afterHideCallback?.Invoke(result);

            return Task.Run(() => result);
        }

        /// <inheritdoc />
        public Task ShowException(Exception e, string title, Action afterHideCallback)
        {
            return Task.Run(() => { new ExceptionMessageBox(e, title).ShowDialog(); });
        }
    }
}