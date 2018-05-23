using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Drachenhorn.Xml.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Dialogs
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
            
            var version = Regex.Match(fileName, "[v]{1}[0-9]*[.]{1}[0-9]*").Value;

            VersionBox.Text = version.Replace("v", "");
            NameBox.Text = fileName.Substring(0, fileName.IndexOf(version, StringComparison.Ordinal) - 1);

            NoButton.Click += (sender, args) =>
            {
                this.Close();
            };
            YesButton.Click += (sender, args) =>
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateImportDialog>();
                logger.Info("Copying " + file.FullName + " to " + DSATemplate.BaseDirectory);

                file.CopyTo(Path.Combine(DSATemplate.BaseDirectory, fileName), true);
                this.Close();
            };
        }
    }
}
