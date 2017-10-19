using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.Interfaces;
using Microsoft.Win32;

namespace DSACharacterSheet.Desktop.IOService
{
    public class SaveIOService : IIOService
    {
        private FileStream fileStream = null;

        public bool CanGetFileStream
        {
            get { return fileStream != null && fileStream.CanWrite; }
        }

        public SaveIOService(SaveFileDialog saveFileDialog)
        {
            if (saveFileDialog.ShowDialog() == true)
                fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        }

        public SaveIOService(string path)
        {
            if (!String.IsNullOrEmpty(path) && File.Exists(path))
                fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        }

        public FileStream GetStream()
        {
            return fileStream;
        }
    }
}
