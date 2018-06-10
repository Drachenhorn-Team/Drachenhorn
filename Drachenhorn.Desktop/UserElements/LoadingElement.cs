using System.Windows;
using System.Windows.Controls;

namespace Drachenhorn.Desktop.UserElements
{
    public class LoadingElement : Control
    {
        static LoadingElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingElement),
                new FrameworkPropertyMetadata(typeof(LoadingElement)));
        }
    }
}