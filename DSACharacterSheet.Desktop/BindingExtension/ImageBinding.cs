using System;
using System.IO;
using System.Windows.Data;
using DSACharacterSheet.Core.Images;

namespace DSACharacterSheet.Desktop.BindingExtension
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
