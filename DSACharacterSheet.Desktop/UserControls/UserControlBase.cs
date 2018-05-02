using DSACharacterSheet.Core.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DSACharacterSheet.Desktop.UserControls
{
    public class UserControlBase : UserControl
    {
        public static DependencyProperty DataProperty =
            DependencyProperty.Register(
                "Data",
                typeof(object),
                typeof(UserControlBase),
                new PropertyMetadata(DataChanged));

        private static void DataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UserControlBase)d).DataChanged(e);
        }

        private void DataChanged(DependencyPropertyChangedEventArgs args)
        {
            if (this.DataContext is ViewModelBase)
                ((ViewModelBase) this.DataContext).Data = Data;
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        public UserControlBase()
        {
            this.Loaded += (sender, args) =>
            {
                this.DataChanged(new DependencyPropertyChangedEventArgs());
            };
        }
    }
}