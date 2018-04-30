using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DSACharacterSheet.Desktop.Lang
{
    public class FlagCultureInfo : CultureInfo
    {
        #region c'tor

        public FlagCultureInfo(string name) : base(name) { }
        public FlagCultureInfo(CultureInfo info) : base(info.Name) { }

        #endregion c'tor

        public BitmapImage FlagSource
        {
            get { return new BitmapImage(new Uri("/DSACharacterSheet.Core;component\\Images\\Flags\\" + Name + ".png")); }
        }
    }
}
