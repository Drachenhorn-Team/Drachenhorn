using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MarkdownSharp;

namespace DSACharacterSheet.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ChangeLogWindow.xaml
    /// </summary>
    public partial class ChangeLogWindow : Window
    {
        public ChangeLogWindow()
        {
            InitializeComponent();

            var markdown = new Markdown();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\changelog.md");
            var transformedText = markdown.Transform(HttpUtility.HtmlEncode(File.ReadAllText(path)));

            Browser.NavigateToString(transformedText);
        }
    }
}
