using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Sheet.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Drachenhorn.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für CoatOfArmsControl.xaml
    /// </summary>
    public partial class CoatOfArmsControl : UserControl
    {
        public CoatOfArmsControl()
        {
            InitializeComponent();
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            T parent = VisualTreeHelper.GetParent(child) as T;

            if (parent != null)
                return parent;
            else
                return FindParent<T>(parent);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is CoatOfArms))
                return;

            var view = new CoatOfArmsPainterView(((CoatOfArms)this.DataContext).Base64String);

            view.Closing += (s, args) =>
            {
                ((CoatOfArms)this.DataContext).Base64String = view.GetBase64();
            };

            view.Show();
        }
    }
}