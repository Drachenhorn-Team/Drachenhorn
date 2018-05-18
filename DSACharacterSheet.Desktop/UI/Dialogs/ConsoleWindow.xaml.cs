using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using log4net.Appender;
using log4net.Core;
using Brushes = System.Windows.Media.Brushes;

namespace DSACharacterSheet.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            this.Visibility = Visibility.Collapsed;

            InitializeComponent();

            RichTextBoxAppender.rtb = RichTextBox;
        }
    }

    public class RichTextBoxAppender : AppenderSkeleton
    {
        internal static RichTextBox rtb;

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (rtb == null) return;
            
            TextRange tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd);
            tr.Text = loggingEvent.RenderedMessage + "\n";

            var color = Brushes.White;

            if (loggingEvent.Level == Level.Debug)
                color = Brushes.DodgerBlue;
            else if (loggingEvent.Level == Level.Info)
                color = Brushes.Green;
            else if (loggingEvent.Level == Level.Warn)
                color = Brushes.Yellow;
            else if (loggingEvent.Level == Level.Error)
                color = Brushes.OrangeRed;
            else if (loggingEvent.Level == Level.Fatal)
                color = Brushes.DarkRed;


            tr.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }
    }
}
