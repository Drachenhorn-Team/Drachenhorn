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
            
            NameBox.Text = fileName;

            using (var sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                sr.ReadLine();
                var secondLine = sr.ReadLine();

                if (!string.IsNullOrEmpty(secondLine))
                {
                    var match = new Regex("Version=\"[0-9]+[.][0-9]+\"").Match(secondLine).Value;
                    VersionBox.Text = match.Substring(9, match.Length - 10);
                }
            }

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
