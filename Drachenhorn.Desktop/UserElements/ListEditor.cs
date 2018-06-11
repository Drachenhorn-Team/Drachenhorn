using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UserElements
{
    public class ListEditor : Control
    {
        public static DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate",
                typeof(DataTemplate),
                typeof(ListEditor));

        public static DependencyProperty ContentTypeProperty =
            DependencyProperty.Register(
                "ContentType",
                typeof(Type),
                typeof(ListEditor));

        public static DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(ListEditor));

        static ListEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListEditor),
                new FrameworkPropertyMetadata(typeof(ListEditor)));
        }

        public DataTemplate ContentTemplate
        {
            get => (DataTemplate) GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }

        public Type ContentType
        {
            get => (Type) GetValue(ContentTypeProperty);
            set => SetValue(ContentTypeProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string) GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ((Button) Template.FindName("PART_AddButton", this)).Click += (sender, args) =>
            {
                (DataContext as IList)?.Add(Activator.CreateInstance(ContentType));
            };

            ((Button) Template.FindName("PART_RemoveButton", this)).Click += (sender, args) =>
            {
                var displayList = (ListBox) Template.FindName("PART_List", this);
                (DataContext as IList)?.Remove(displayList.SelectedItem);
            };
        }
    }
}