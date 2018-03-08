using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.FileReader;

namespace DSACharacterSheet.Core.IO
{
    public interface IIOService
    {
        void SaveAsCharacterSheet(CharacterSheet sheet);
        CharacterSheet OpenCharacterSheet();
    }
}
