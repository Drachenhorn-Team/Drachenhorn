using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Xml.Sheet;

namespace Drachenhorn.Xml.Calculation
{
    public class CalculateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the sheet.
        /// </summary>
        /// <value>
        /// The sheet.
        /// </value>
        public CharacterSheet Sheet { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateEventArgs"/> class.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        public CalculateEventArgs(CharacterSheet sheet)
        {
            Sheet = sheet;
        }
    }
}
