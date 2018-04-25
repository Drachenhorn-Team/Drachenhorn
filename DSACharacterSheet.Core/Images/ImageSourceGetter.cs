using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSACharacterSheet.Core.Images
{
    public class ImageSourceGetter
    {
        public string this[string path]
        {
            get
            {
                var temp = Path.Combine(Environment.CurrentDirectory, "Images", Path.Combine(path.Split('/')));
                return temp;
            }
        }
    }
}
