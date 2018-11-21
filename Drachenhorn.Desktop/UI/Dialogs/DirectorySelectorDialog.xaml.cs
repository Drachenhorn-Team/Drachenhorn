using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für DirectorySelectorDialog.xaml
    /// </summary>
    public partial class DirectorySelectorDialog
    {
        public string SelectedFile { get; set; }

        public DirectorySelectorDialog(string dir, string ext, string title)
        {
            InitializeComponent();

            this.Title = title;

            List<FileInfo> result = new List<FileInfo>();

            if (!string.IsNullOrEmpty(ext))
                foreach (var file in new DirectoryInfo(dir).GetFiles())
                {
                    if (file.FullName.EndsWith(ext.Replace("*", "")))
                        result.Add(file);
                }

            Resources["FileList"] = result;
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FileBox.SelectedItem == null) return;

            SelectedFile = (FileBox.SelectedItem as FileInfo)?.FullName;

            DialogResult = true;
            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
