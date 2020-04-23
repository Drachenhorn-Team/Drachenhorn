using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Drachenhorn.Desktop.Views;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Desktop.UserElements
{
    public class InfoButton : Control
    {
        public static DependencyProperty InfoObjectProperty =
            DependencyProperty.Register(
                "InfoObject",
                typeof(IInfoObject),
                typeof(InfoButton),
                new PropertyMetadata(BindingChanged));

        public static DependencyProperty CanShowInfoProperty =
            DependencyProperty.Register(
                "CanShowInfo",
                typeof(bool),
                typeof(InfoButton));

        #region c'tor

        static InfoButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoButton),
                new FrameworkPropertyMetadata(typeof(InfoButton)));
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var button = Template.FindName("PART_InfoButton", this) as Button;
            if (button != null) button.Click += (s, a) => { new InfoView(InfoObject).ShowDialog(); };
        }

        public static void BindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((InfoButton) d).BindingChanged(args);
        }

        private void BindingChanged(DependencyPropertyChangedEventArgs args)
        {
            CanShowInfo = InfoObject?.GetInformation()?.Any(x => x.Key != "%Info.Name") == true;
        }

        #region Properties

        public IInfoObject InfoObject
        {
            get => (IInfoObject) GetValue(InfoObjectProperty);
            set => SetValue(InfoObjectProperty, value);
        }

        public bool CanShowInfo
        {
            get => (bool) GetValue(CanShowInfoProperty);
            set => SetValue(CanShowInfoProperty, value);
        }

        #endregion
    }
}