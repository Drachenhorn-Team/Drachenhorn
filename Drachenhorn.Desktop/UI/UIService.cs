using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Drachenhorn.Core.UI;
using Drachenhorn.Xml;

namespace Drachenhorn.Desktop.UI
{
    public class UIService : BindableBase, IUIService
    {
        private bool _isBusy;

        public void SetBusyState()
        {
            SetBusyState(true);
        }

        private void SetBusyState(bool busy)
        {
            if (busy != _isBusy)
            {
                _isBusy = busy;
                Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                if (_isBusy)
                {
                    new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.ContextIdle,
                        dispatcherTimer_Tick, Application.Current.Dispatcher);
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var dispatcherTimer = sender as DispatcherTimer;
            if (dispatcherTimer != null)
            {
                SetBusyState(false);
                dispatcherTimer.Stop();
            }
        }
    }
}
