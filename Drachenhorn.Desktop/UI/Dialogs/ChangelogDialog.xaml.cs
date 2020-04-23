using System.Collections.Generic;
using System.Windows;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    public partial class ChangelogDialog
    {
        public ChangelogDialog(IEnumerable<string> changes)
        {
            Changes = changes;

            InitializeComponent();
            DataContext = this;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #region Properties

        private string _currentVersion;

        public string CurrentVersion
        {
            get => _currentVersion;
            set
            {
                if (_currentVersion == value)
                    return;
                _currentVersion = value;
            }
        }

        private string _newVersion;

        public string NewVersion
        {
            get => _newVersion;
            set
            {
                if (_newVersion == value)
                    return;
                _newVersion = value;
            }
        }

        private IEnumerable<string> _changes;

        public IEnumerable<string> Changes
        {
            get => _changes;
            set
            {
                if (_changes == value)
                    return;
                _changes = value;
            }
        }

        #endregion Properties
    }
}