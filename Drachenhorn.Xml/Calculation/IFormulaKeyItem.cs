namespace Drachenhorn.Xml.Calculation
{
    /// <summary>
    ///     Interface to use as Parameter in Calculation.
    /// </summary>
    public interface IFormulaKeyItem
    {
        /// <summary>
        ///     Gets the key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        string Key { get; }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        int Value { get; }
    }
}