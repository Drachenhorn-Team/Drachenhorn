using System;
using Drachenhorn.Xml.Sheet;

namespace Drachenhorn.Xml.Calculation
{
    /// <summary>
    ///     CalculationEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CalculateEventArgs : EventArgs
    {
        #region c'tor

        /// <summary>
        ///     Initializes a new instance of the <see cref="CalculateEventArgs" /> class.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        public CalculateEventArgs(CharacterSheet sheet)
        {
            Sheet = sheet;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the sheet.
        /// </summary>
        /// <value>
        ///     The sheet.
        /// </value>
        public CharacterSheet Sheet { get; }

        #endregion
    }
}