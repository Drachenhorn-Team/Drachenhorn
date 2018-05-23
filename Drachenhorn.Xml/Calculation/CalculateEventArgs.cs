using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Xml.Sheet;

namespace Drachenhorn.Xml.Calculation
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
