using System.ComponentModel;

namespace Drachenhorn.Xml.Interfaces
{
    /// <summary>
    ///     Interface to note if a child has changed.
    /// </summary>
    public interface INotifyChildChanged
    {
        /// <summary>
        ///     Occurs when [child changed].
        /// </summary>
        event PropertyChangedEventHandler ChildChanged;
    }
}