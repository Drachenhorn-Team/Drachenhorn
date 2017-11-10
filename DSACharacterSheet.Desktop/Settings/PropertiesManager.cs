using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Desktop.Settings
{
    public static class PropertiesManager
    {
        private static Properties _properties;
        public static Properties Properties
        {
            get { return _properties; }
            private set
            {
                if (_properties == value)
                    return;
                _properties = value;
            }
        }

        internal static void Initialize()
        {
            Properties = Properties.Load();
        }
    }
}
