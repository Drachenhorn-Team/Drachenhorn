using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interaktionslogik für ExceptionMessageBox.xaml
    /// </summary>
    public partial class ExceptionMessageBox
    {
        private readonly string _userExceptionMessage;
        private readonly List<string> _exceptionInformationList = new List<string>();

        public ExceptionMessageBox(Exception e, string userExceptionMessage, bool closeApp = false)
        {
            InitializeComponent();

            this._userExceptionMessage = userExceptionMessage;
            textBox1.Text = userExceptionMessage;

            var treeViewItem = new TreeViewItem
            {
                Header = "Exception"
            };
            treeViewItem.ExpandSubtree();
            BuildTreeLayer(e, treeViewItem);
            treeView1.Items.Add(treeViewItem);

            this.Closed += (sender, args) => { Application.Current.Shutdown(2); };
        }

        private void BuildTreeLayer(Exception e, TreeViewItem parent)
        {
            String exceptionInformation = "\n\r\n\r" + e.GetType().ToString() + "\n\r\n\r";
            parent.DisplayMemberPath = "Header";
            parent.Items.Add(new TreeViewStringSet() { Header = "Type", Content = e.GetType().ToString() });
            System.Reflection.PropertyInfo[] memberList = e.GetType().GetProperties();
            foreach (PropertyInfo info in memberList)
            {
                var value = info.GetValue(e, null);
                if (value != null)
                {
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
                        TreeViewStringSet treeViewStringSet = new TreeViewStringSet() { Header = info.Name, Content = value.ToString() };
                        parent.Items.Add(treeViewStringSet);
                        exceptionInformation += treeViewStringSet.Header + "\n\r\n\r" + treeViewStringSet.Content + "\n\r\n\r";
                    }
                }
            }
            _exceptionInformationList.Add(exceptionInformation);
        }

        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(TreeViewItem)) textBox1.Text = "Exception";
            else textBox1.Text = e.NewValue.ToString();
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

        private void ButtonClipboard_Click(object sender, RoutedEventArgs e)
        {
            string clipboardMessage = _userExceptionMessage + "\n\r\n\r";
            foreach (string info in _exceptionInformationList) clipboardMessage += info;
            Clipboard.SetText(clipboardMessage);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}