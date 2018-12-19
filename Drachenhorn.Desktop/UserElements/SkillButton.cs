using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UserElements
{
    public class SkillButton : Button
    {
        #region c'tor

        public SkillButton()
        {
            DataContext = this;
            SetBinding(ContentProperty, "Value");
        }

        #endregion

        #region ValueProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(SkillButton),
                new PropertyMetadata(-1, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SkillButton) d).OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            Content = e.NewValue;
        }

        public int Value
        {
            get => (int) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion ValueProperty
    }
}