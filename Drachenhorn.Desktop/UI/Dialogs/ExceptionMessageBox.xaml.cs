using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    ///     Interaktionslogik für ExceptionMessageBox.xaml
    /// </summary>
    public partial class ExceptionMessageBox
    {
        private readonly List<string> _exceptionInformationList = new List<string>();
        private readonly string _userExceptionMessage;

        public ExceptionMessageBox(Exception e, string userExceptionMessage, bool closeApp = false)
        {
            InitializeComponent();

            _userExceptionMessage = userExceptionMessage;
            textBox1.Text = userExceptionMessage;

            var treeViewItem = new TreeViewItem
            {
                Header = "Exception"
            };
            treeViewItem.ExpandSubtree();
            BuildTreeLayer(e, treeViewItem);
            treeView1.Items.Add(treeViewItem);

            Closed += (sender, args) => { Application.Current.Shutdown(2); };
        }

        private void BuildTreeLayer(Exception e, TreeViewItem parent)
        {
            var exceptionInformation = "\n\r\n\r" + e.GetType() + "\n\r\n\r";
            parent.DisplayMemberPath = "Header";
            parent.Items.Add(new TreeViewStringSet {Header = "Type", Content = e.GetType().ToString()});
            var memberList = e.GetType().GetProperties();
            foreach (var info in memberList)
            {
                var value = info.GetValue(e, null);
                if (value != null)
                    if (info.Name == "InnerException")
                    {
                        var treeViewItem = new TreeViewItem
                        {
                            Header = info.Name
                        };
                        BuildTreeLayer(e.InnerException, treeViewItem);
                        parent.Items.Add(treeViewItem);
                    }
                    else
                    {
                        var treeViewStringSet = new TreeViewStringSet {Header = info.Name, Content = value.ToString()};
                        parent.Items.Add(treeViewStringSet);
                        exceptionInformation += treeViewStringSet.Header + "\n\r\n\r" + treeViewStringSet.Content +
                                                "\n\r\n\r";
                    }
            }

            _exceptionInformationList.Add(exceptionInformation);
        }

        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(TreeViewItem)) textBox1.Text = "Exception";
            else textBox1.Text = e.NewValue.ToString();
        }

        private void ButtonClipboard_Click(object sender, RoutedEventArgs e)
        {
            var clipboardMessage = _userExceptionMessage + "\n\r\n\r";
            foreach (var info in _exceptionInformationList) clipboardMessage += info;
            Clipboard.SetText(clipboardMessage);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private class TreeViewStringSet
        {
            public string Header { get; set; }
            public string Content { get; set; }

            public override string ToString()
            {
                return Content;
            }
        }
    }
}