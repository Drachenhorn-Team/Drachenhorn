using System;
using System.Collections.Generic;
using System.Text;

namespace Drachenhorn.Xml.Calculation
{
    public interface IFormulaKeyItem
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        double Value { get; }
    }
}
