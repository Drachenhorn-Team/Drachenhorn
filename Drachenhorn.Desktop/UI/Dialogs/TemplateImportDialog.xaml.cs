using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Drachenhorn.Xml.Data;
using Drachenhorn.Xml.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für TemplateImportDialog.xaml
    /// </summary>
    public partial class TemplateImportDialog : Window
    {
        #region c'tor

        public TemplateImportDialog(IEnumerable<FileInfo> files)
        {
            InitializeComponent();

            List<TemplateMetadata> templates = new List<TemplateMetadata>();

            foreach (var file in files)
                templates.Add(new TemplateMetadata(file.FullName));

            ItemList.ItemsSource = templates;

            //var fileName = file.Name;

            //NameBox.Text = fileName;

            //using (var sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            //{
            //    sr.ReadLine();
            //    var secondLine = sr.ReadLine();

            //    if (!string.IsNullOrEmpty(secondLine))
            //    {
            //        var match = new Regex("Version=\"[0-9]+[.][0-9]+\"").Match(secondLine).Value;
            //        VersionBox.Text = match.Substring(9, match.Length - 10);
            //    }
            //}

            NoButton.Click += (sender, args) => { Close(); };
            YesButton.Click += (sender, args) =>
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateImportDialog>();

                foreach (var template in templates)
                {
                    logger.Info("Copying " + template.Path + " to " + Constants.TemplateBaseDirectory);

                    File.Copy(template.Path,
                        Path.Combine(Constants.TemplateBaseDirectory, template.Name + Constants.TemplateExtension),
                        false);
                }
                Close();
            };
        }

        #endregion
    }
}