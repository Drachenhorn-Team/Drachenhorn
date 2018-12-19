using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using log4net.Appender;
using log4net.Core;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        #region c'tor

        public ConsoleWindow()
        {
            Visibility = Visibility.Collapsed;

            InitializeComponent();

            RichTextBoxAppender.rtb = RichTextBox;
        }

        #endregion

        #region Properties

        public bool ShouldClose = false;

        #endregion

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void RichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ConsoleScroll.ScrollToEnd();
        }

        private void ConsoleWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ConsoleWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!ShouldClose)
                e.Cancel = true;
        }
    }

    public class RichTextBoxAppender : AppenderSkeleton
    {
        internal static RichTextBox rtb;

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (rtb == null) return;

            var p = new Paragraph();

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