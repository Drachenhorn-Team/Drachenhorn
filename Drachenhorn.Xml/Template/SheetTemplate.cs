﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Drachenhorn.Xml.Data.AP;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    ///     Templating Class
    /// </summary>
    /// <seealso cref="T:Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    [XmlRoot("Template")]
    public class SheetTemplate : TemplateMetadata, ISavable
    {
        #region c'tor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SheetTemplate" /> class.
        /// </summary>
        public SheetTemplate()
        {
            Name = "New";

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "HasChanged") HasChanged = true;
            };
            ChildChanged += (sender, args) => { HasChanged = true; };
        }

        #endregion c'tor

        #region Static

        /// <summary>
        ///     Gets the available templates.
        /// </summary>
        /// <value>
        ///     The available templates.
        /// </value>
        public static IEnumerable<TemplateMetadata> AvailableTemplates
        {
            get
            {
                if (!Directory.Exists(BaseDirectory))
                    Directory.CreateDirectory(BaseDirectory);

                var result = new List<TemplateMetadata>();
                var files = Directory.GetFiles(BaseDirectory);

                foreach (var file in files)
                {
                    if (!file.EndsWith(Extension))
                        continue;

                    using (var sr = new StreamReader(File.OpenRead(file)))
                    {
                        sr.ReadLine();

                        result.Add(new TemplateMetadata(
                            new FileInfo(file).Name.Replace(Extension, ""),
                            sr.ReadLine()));
                    }
                }

                return result;
            }
        }

        #endregion Static

        #region Properties

        [XmlIgnore] private APTable _apTable = new APTable();

        /// <summary>
        ///     Gets or sets the AP-Table.
        /// </summary>
        /// <value>
        ///     The AP-Table.
        /// </value>
        [XmlElement("APTable")]
        public APTable APTable
        {
            get => _apTable;
            set
            {
                if (_apTable == value)
                    return;
                _apTable = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private ObservableCollection<RaceInformation> _races = new ObservableCollection<RaceInformation>();

        /// <summary>
        ///     Gets or sets the Races.
        /// </summary>
        /// <value>
        ///     The Races.
        /// </value>
        [XmlElement("Race")]
        public ObservableCollection<RaceInformation> Races
        {
            get => _races;
            set
            {
                if (_races == value)
                    return;
                _races = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private ObservableCollection<CultureInformation> _cultures =
            new ObservableCollection<CultureInformation>();

        /// <summary>
        ///     Gets or sets the Cultures.
        /// </summary>
        /// <value>
        ///     The Cultures.
        /// </value>
        [XmlElement("Culture")]
        public ObservableCollection<CultureInformation> Cultures
        {
            get => _cultures;
            set
            {
                if (_cultures == value)
                    return;
                _cultures = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<SpecialSkill> _specialSkills = new ObservableCollection<SpecialSkill>();

        /// <summary>
        ///     Gets or sets the special skills.
        /// </summary>
        /// <value>
        ///     The special skills.
        /// </value>
        [XmlElement("SpecialSkill")]
        public ObservableCollection<SpecialSkill> SpecialSkills
        {
            get => _specialSkills;
            set
            {
                if (_specialSkills == value)
                    return;
                _specialSkills = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Save/Load

        /// <summary>
        ///     Gets the Template BaseDirectory.
        /// </summary>
        /// <value>
        ///     The Template BaseDirectory.
        /// </value>
        public static string BaseDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Drachenhorn", "Templates");

        /// <summary>
        ///     The Template Extension
        /// </summary>
        public static readonly string Extension = ".dsat";

        /// <summary>
        ///     Gets the Current Template FilePath.
        /// </summary>
        /// <value>
        ///     The Current Template FilePath.
        /// </value>
        [XmlIgnore]
        public string FilePath
        {
            get => Path.Combine(BaseDirectory, Name + Extension);
            private set
            {
                if (value.StartsWith(BaseDirectory))
                    Name = value.Replace(BaseDirectory, "").Replace("\\", "").Replace(Extension, "");
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Loads a CharacterSheet from a selected path.
        /// </summary>
        /// <param name="fileName">Name of Template File</param>
        /// <returns>Loaded CharacterSheet</returns>
        public static SheetTemplate Load(string fileName)
        {
            var path = Path.Combine(BaseDirectory, fileName + Extension);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var serializer = new XmlSerializer(typeof(SheetTemplate));
                var temp = (SheetTemplate) serializer.Deserialize(stream);
                temp.FilePath = path;
                temp.HasChanged = false;

                return temp;
            }
        }

        /// <summary>
        ///     Saves the current Template to a selected path.
        /// </summary>
        /// <returns><c>True</c> if successful, otherwise <c>False</c></returns>
        public bool Save()
        {
            return Save(FilePath);
        }

        /// <summary>
        ///     Saves the current Template to a given path.
        /// </summary>
        /// <param name="path">Path to save to.</param>
        /// <returns><c>True</c> if successful, otherwise <c>False</c>.</returns>
        public bool Save(string path)
        {
            using (var stream = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(SheetTemplate));
                serializer.Serialize(stream, this);
                HasChanged = false;
            }

            return true;
        }

        /// <inheritdoc />
        void ISavable.Save(string path)
        {
            Save(path);
        }

        #endregion Save/Load

        #region HasChanged

        [XmlIgnore] private bool _hasChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has changed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has changed; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
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

        #endregion HasChanged
    }
}