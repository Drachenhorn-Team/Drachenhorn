using System.Windows;

namespace Drachenhorn.Desktop.Helper
{
    public class BindingProxy : Freezable
    {
        // Using a DependencyProperty as the backing store for Data.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
                typeof(BindingProxy), new UIPropertyMetadata(null));

        #region Properties

        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        #endregion

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}