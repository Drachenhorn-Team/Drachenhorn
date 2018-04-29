using System;
using System.Collections.Generic;
using System.Text;

namespace DSACharacterSheet.Xml.Calculation
{
    public interface IFormulaKeyItem
    {
        string Key { get; }
        double Value { get; }
    }
}
