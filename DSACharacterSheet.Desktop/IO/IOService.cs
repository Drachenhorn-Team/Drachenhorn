using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.FileReader;
using Microsoft.Win32;

namespace DSACharacterSheet.Desktop.IO
{
    public class IOService : IIOService
    {
        public void SaveAsCharacterSheet(CharacterSheet sheet)
        {
            var fileDialog = new SaveFileDialog
            {
                FileName = string.IsNullOrEmpty(sheet.Name)
                    ? "Charakterbogen"
                    : sheet.Name,
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac",
                FilterIndex = 1,
                AddExtension = true,
                Title = "Charakterbogen speichern"
            };

            if (fileDialog.ShowDialog() != true) return;
        }

        public CharacterSheet OpenCharacterSheet()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "DSA-Charakterbogen (*.dsac)|*.dsac|Alle Dateien (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                Title = "Charakterbogen öffnen.",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            return fileDialog.ShowDialog() != true ? null : CharacterSheet.Load(fileDialog.FileName);
        }
    }
}
