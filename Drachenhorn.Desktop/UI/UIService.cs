using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Drachenhorn.Core.UI;
using Drachenhorn.Xml;

namespace Drachenhorn.Desktop.UI
{
    public class UIService : BindableBase, IUIService
    {
        #region Properties

        private bool _isBusy;

        #endregion

        public void SetBusyState()
        {
            SetBusyState(true);
        }

        private void SetBusyState(bool busy, int seconds = 5)
        {
            if (busy != _isBusy)
            {
                _isBusy = busy;
                Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                if (_isBusy)
                    new DispatcherTimer(TimeSpan.FromSeconds(seconds), DispatcherPriority.ContextIdle,
                        dispatcherTimer_Tick, Application.Current.Dispatcher);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dispatcherTimer)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }
    }
}