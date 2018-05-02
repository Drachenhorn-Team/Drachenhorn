using DSACharacterSheet.Core.Images;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.UI.BindingExtension
{
    public class ImageBinding : Binding
    {
        /// <summary>
        /// Translates the given TranslateID
        /// </summary>
        /// <param name="path">TranslateID</param>
        public ImageBinding(string path) : base("[" + path + "]")
        {
            this.Source = new ImageSourceGetter();
        }
    }
}