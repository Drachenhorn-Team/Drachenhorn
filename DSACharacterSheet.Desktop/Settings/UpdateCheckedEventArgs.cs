using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Desktop.Settings
{
    public class UpdateCheckedEventArgs : EventArgs
    {
        private bool _isUpdateAvailable = false;
        public bool IsUpdateAvailable
        {
            get { return _isUpdateAvailable; }
            private set
            {
                if (_isUpdateAvailable == value)
                    return;
                _isUpdateAvailable = value;
            }
        }

        public UpdateCheckedEventArgs(bool isUpdateAvailable)
        {
            IsUpdateAvailable = isUpdateAvailable;
        }
    }

    public delegate void UpdateCheckedHandler(object sender, UpdateCheckedEventArgs args);
}
