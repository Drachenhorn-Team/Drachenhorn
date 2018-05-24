using System.Collections.Generic;
using System.ComponentModel;

namespace Drachenhorn.Xml.Interfaces
{
    /// <summary>
    /// Interface to display information about a Class.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IInfoObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the relevant information of the Object.
        /// </summary>
        /// <returns>Dictionary of information name (not translated) to information</returns>
        Dictionary<string, string> GetInformation();
    }
}