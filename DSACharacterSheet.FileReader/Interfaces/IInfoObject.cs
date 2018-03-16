using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.FileReader.Interfaces
{
    public interface IInfoObject : INotifyPropertyChanged
    {
        Dictionary<string, string> GetInformation();
    }
}
