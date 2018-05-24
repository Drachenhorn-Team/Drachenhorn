using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Drachenhorn.Xml.Interfaces
{
    /// <summary>
    /// Interface to note if a child has changed.
    /// </summary>
    public interface INotifyChildChanged
    {
        /// <summary>
        /// Occurs when [child changed].
        /// </summary>
        event PropertyChangedEventHandler ChildChanged;
    }
}
