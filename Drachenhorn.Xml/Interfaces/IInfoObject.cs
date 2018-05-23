using System.Collections.Generic;
using System.ComponentModel;

namespace Drachenhorn.Xml.Interfaces
{
    public interface IInfoObject : INotifyPropertyChanged
    {
        Dictionary<string, string> GetInformation();
    }
}