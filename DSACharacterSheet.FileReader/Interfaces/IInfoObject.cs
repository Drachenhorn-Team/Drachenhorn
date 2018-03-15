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
        string Value { get; set; }

        bool CanShowInfo { get; }


        Dictionary<string, string> GetInformation();
    }
}
