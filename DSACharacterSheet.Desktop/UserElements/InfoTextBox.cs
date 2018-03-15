using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
using DSACharacterSheet.FileReader.Interfaces;
using ThicknessConverter = Xceed.Wpf.DataGrid.Converters.ThicknessConverter;

namespace DSACharacterSheet.Desktop.UserElements
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DSACharacterSheet.Desktop.UserElements"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DSACharacterSheet.Desktop.UserElements;assembly=DSACharacterSheet.Desktop.UserElements"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Navigieren Sie zu diesem Projekt, und wählen Sie es aus.]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:InfoTextBox/>
    ///
    /// </summary>
    public class InfoTextBox : TextBox
    {
        #region Properties

        public static DependencyProperty BindingProperty =
            DependencyProperty.Register(
                "Binding",
                typeof(IInfoObject),
                typeof(InfoTextBox),
                new PropertyMetadata(OnBindingChanged));

        public IInfoObject Binding
        {
            get { return (IInfoObject) GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }

        protected static void OnBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((InfoTextBox)d).OnBindingChanged((IInfoObject)e.NewValue);
        }

        protected void OnBindingChanged(IInfoObject newValue)
        {

        }

        #endregion Properties

        #region c'tor

        static InfoTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoTextBox), new FrameworkPropertyMetadata(typeof(InfoTextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DataContext = this;
            var button = Template.FindName("PART_InfoButton", this) as Button;
            if (button != null)
            {
                button.Click += (s, a) =>
                {

                };
            }
        }

        #endregion c'tor
    }
}
