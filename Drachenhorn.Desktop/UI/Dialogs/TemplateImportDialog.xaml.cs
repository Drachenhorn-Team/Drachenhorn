using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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

        public TemplateImportDialog(TemplateMetadata template) : this(new[] { template }) { }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public TemplateImportDialog(IEnumerable<TemplateMetadata> templates)
        {
            InitializeComponent();

            ItemList.ItemsSource = templates;

            NoButton.Click += (sender, args) => { Close(); };
            YesButton.Click += (sender, args) =>
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>().GetLogger<TemplateImportDialog>();

                foreach (var template in templates)
                {
                    logger.Info("Copying " + template.Path + " to " + Constants.TemplateBaseDirectory);

                    template.CopyToTemplateDirectory();
                }
                Close();
            };
        }

        #endregion
    }
}