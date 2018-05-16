using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DSACharacterSheet.Xml.Interfaces
{
    public interface INotifyChildChanged
    {
        event PropertyChangedEventHandler ChildChanged;
    }
}
