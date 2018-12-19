using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für DirectorySelectorDialog.xaml
    /// </summary>
    public partial class DirectorySelectorDialog
    {
        #region c'tor

        public DirectorySelectorDialog(string dir, string ext, string title)
        {
            InitializeComponent();

            Title = title;

            var result = new List<FileInfo>();

            if (!string.IsNullOrEmpty(ext))
                foreach (var file in new DirectoryInfo(dir).GetFiles())
                    if (file.FullName.EndsWith(ext.Replace("*", "")))
                        result.Add(file);

            Resources["FileList"] = result;
        }

        #endregion

        #region Properties

        public string SelectedFile { get; set; }

        #endregion

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FileBox.SelectedItem == null) return;

            SelectedFile = (FileBox.SelectedItem as FileInfo)?.FullName;

            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}