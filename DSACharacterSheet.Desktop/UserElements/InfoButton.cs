using System;
using System.Collections.Generic;
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
using DSACharacterSheet.Desktop.Views;
using DSACharacterSheet.FileReader.Interfaces;

namespace DSACharacterSheet.Desktop.UserElements
{
    public class InfoButton : Control
    {
        #region Properties

        public static DependencyProperty InfoObjectProperty =
            DependencyProperty.Register(
                "InfoObject",
                typeof(IInfoObject),
                typeof(InfoButton),
                new PropertyMetadata(BindingChanged));

        public IInfoObject InfoObject
        {
            get { return (IInfoObject)GetValue(InfoObjectProperty); }
            set { SetValue(InfoObjectProperty, value); }
        }

        public static void BindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((InfoButton)d).BindingChanged(args);
        }

        private void BindingChanged(DependencyPropertyChangedEventArgs args)
        {
            CanShowInfo = InfoObject?.GetInformation()?.Any(x => x.Key != "%Info.Name") == true;
        }

        public static DependencyProperty CanShowInfoProperty =
            DependencyProperty.Register(
                "CanShowInfo",
                typeof(bool),
                typeof(InfoButton));

        public bool CanShowInfo
        {
            get { return (bool)GetValue(CanShowInfoProperty); }
            set { SetValue(CanShowInfoProperty, value); }
        }

        #endregion Properties

        static InfoButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoButton), new FrameworkPropertyMetadata(typeof(InfoButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var button = Template.FindName("PART_InfoButton", this) as Button;
            if (button != null)
            {
                button.Click += (s, a) => { new InfoView(InfoObject).Show(); };
            }
        }
    }
}
