using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UserElements
{
    public class SkillButton : Button
    {
        #region ValueProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(SkillButton),
            new PropertyMetadata(-1, new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SkillButton)d).OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Content = e.NewValue;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion ValueProperty

        public SkillButton()
        {
            this.DataContext = this;
            this.SetBinding(Button.ContentProperty, "Value");
        }
    }
}