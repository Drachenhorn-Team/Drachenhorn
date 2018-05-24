using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Drachenhorn.Xml.Interfaces
{
    public interface INotifyChildChanged
    {
        /// <summary>
        /// Occurs when [child changed].
        /// </summary>
        event PropertyChangedEventHandler ChildChanged;
    }
}
