using DSACharacterSheet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace DSACharacterSheet.Desktop.IOService
{
    public class LoadIOService : IIOService
    {
        private FileStream fileStream = null;

        public bool CanGetFileStream
        {
            get { return fileStream != null && fileStream.CanWrite; }
        }

        public LoadIOService(OpenFileDialog openFileDialog)
        {
            if (openFileDialog.ShowDialog() == true)
                fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public LoadIOService(string path)
        {
            if (!String.IsNullOrEmpty(path) && File.Exists(path))
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public FileStream GetStream()
        {
            return fileStream;
        }
    }
}
