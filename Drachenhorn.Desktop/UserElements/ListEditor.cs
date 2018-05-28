using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;

namespace Drachenhorn.Desktop.UserElements
{
    public class ListEditor : Control
    {
        public static DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(ListEditor));

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        
        public static DependencyProperty ContentTypeProperty =
            DependencyProperty.Register(
                "ContentType",
                typeof(Type),
                typeof(ListEditor));

        public Type ContentType
        {
            get { return (Type)GetValue(ContentTypeProperty); }
            set { SetValue(ContentTypeProperty, value); }
        }

        public static DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(String),
                typeof(ListEditor));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        static ListEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListEditor), new FrameworkPropertyMetadata(typeof(ListEditor)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ((Button) Template.FindName("PART_AddButton", this)).Click += (sender, args) =>
            {
                (this.DataContext as IList)?.Add(Activator.CreateInstance(ContentType));
            };

            ((Button)Template.FindName("PART_RemoveButton", this)).Click += (sender, args) =>
            {
                var displayList = (ListBox)Template.FindName("PART_List", this);
                (this.DataContext as IList)?.Remove(displayList.SelectedItem);
            };
        }
    }
}
