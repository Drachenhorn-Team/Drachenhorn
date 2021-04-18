using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Files.File
{
    [Serializable]
    public abstract class SerializableObject : BindableBase
    {
        #region Properties

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath == value)
                    return;
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private bool _hasChanged;
        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                if (_hasChanged == value)
                    return;
                _hasChanged = value;
                OnPropertyChanged();
            }
        }

        #endregion


        /// <summary>
        ///     Loads a CharacterSheet from a selected path
        /// </summary>
        /// <typeparam name="T">Type to be serialized to</typeparam>
        /// <param name="path">Path to file</param>
        /// <returns>Loaded CharacterSheet</returns>
        /// <exception cref="IOException" />
        public static T Load<T>(string path) where T : SerializableObject
        {
            try
            {
                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(typeof(T));
                var temp = (T)serializer.Deserialize(stream);
                temp.FilePath = path;

                temp.HasChanged = false;

                return temp;
            }
            catch (IOException e)
            {
                throw new IOException("Unable to load '" + path + "'", e);
            }
        }

        /// <summary>
        ///     Saves this Sheet.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if successful; otherwise, <c>false</c>
        /// </returns>
        public bool Save()
        {
            return !string.IsNullOrEmpty(FilePath) && Save(FilePath);
        }

        /// <summary>
        ///     Saves the current CharacterSheet to a selected path.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <exception cref="IOException" />
        public bool Save(string path)
        {
            try
            {
                using var stream = new StreamWriter(path);
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
                HasChanged = false;

                return true;
            }
            catch (IOException e)
            {
                throw new IOException("Unable to save '" + path + "'", e);
            }
        }
    }
}
