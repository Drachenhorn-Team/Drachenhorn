using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using DSACharacterSheet.Xml.Template;

namespace DSACharacterSheet.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für TemplateImportDialog.xaml
    /// </summary>
    public partial class TemplateImportDialog : Window
    {
        public TemplateImportDialog(string filePath)
        {
            InitializeComponent();

            var file = new FileInfo(filePath);

            var fileName = file.Name;
            
            var version = Regex.Match(fileName, "[v]{1}[0-9]*.[0-9]*").Value;

            VersionBox.Text = version.Replace("v", "");
            NameBox.Text = fileName.Substring(0, fileName.IndexOf(version, StringComparison.Ordinal) - 1);

            NoButton.Click += (sender, args) => { this.Close(); };
            YesButton.Click += (sender, args) => { file.CopyTo(Path.Combine(DSATemplate.BaseDirectory, fileName)); };
        }
    }
}
