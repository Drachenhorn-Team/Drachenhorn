using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using log4net.Appender;
using log4net.Core;
using Brushes = System.Windows.Media.Brushes;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public ConsoleWindow()
        {
            this.Visibility = Visibility.Collapsed;

            InitializeComponent();

            RichTextBoxAppender.rtb = RichTextBox;
        }

        private void RichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ConsoleScroll.ScrollToEnd();
        }

        private void ConsoleWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }

    public class RichTextBoxAppender : AppenderSkeleton
    {
        internal static RichTextBox rtb;

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (rtb == null) return;

            Paragraph p = new Paragraph();

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

            p.Inlines.Add(new Run(RenderLoggingEvent(loggingEvent).TrimEnd('\r', '\n')) {Foreground = color});

            if (rtb.Dispatcher.CheckAccess())
                rtb.Document.Blocks.Add(p);
        }
    }
}
