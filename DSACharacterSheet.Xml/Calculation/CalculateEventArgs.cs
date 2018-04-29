using System;
using System.Collections.Generic;
using System.Text;
using DSACharacterSheet.Xml.Sheet;

namespace DSACharacterSheet.Xml.Calculation
{
    public class CalculateEventArgs : EventArgs
    {
        public CharacterSheet Sheet { get; private set; }

        public CalculateEventArgs(CharacterSheet sheet)
        {
            Sheet = sheet;
        }
    }
}
