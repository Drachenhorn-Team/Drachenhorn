using System.Collections.Generic;
using System.ComponentModel;

namespace DSACharacterSheet.Xml.Interfaces
{
    public interface IInfoObject : INotifyPropertyChanged
    {
        Dictionary<string, string> GetInformation();
    }
}