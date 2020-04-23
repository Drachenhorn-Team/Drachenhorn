using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drachenhorn.Core.UI
{
    public interface IDialogService
    {
        Task<int> ShowMessage(string message, string title = null, IEnumerable<string> buttons = null,
            Action<int> afterHideCallback = null);

        Task<int> ShowMessageExternal(string message, string title = null, IEnumerable<string> buttons = null,
            Action<int> afterHideCallback = null);


        Task ShowException(Exception e, string title, Action afterHideCallback);
    }
}