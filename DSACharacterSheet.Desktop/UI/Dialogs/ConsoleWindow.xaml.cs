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

namespace DSACharacterSheet.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public ConsoleWindow()
        {
            InitializeComponent();

            Console.SetOut(new ConsoleLogger(RichTextBox));
        }
    }

    internal class ConsoleLogger : TextWriter
    {
        private RichTextBox rtb;

        public ConsoleLogger(RichTextBox rtb)
        {
            this.rtb = rtb;
        }

        public override Encoding Encoding
        {
            get { return null; }
        }

        public override void Write(char value)
        {
            if (value != '\r') rtb.AppendText(new string(value, 1));
        }
    }
}
