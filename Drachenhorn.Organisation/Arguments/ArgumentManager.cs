using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mono.Options;

namespace Drachenhorn.Organisation.Arguments
{
    public class ArgumentManager
    {
        #region Properties

        public bool ShouldPrint { get; private set; } = false;

        private Dictionary<string, List<FileInfo>> Files { get; set; }

        /// <summary>
        ///     Return the file with the given File Extension
        /// </summary>
        /// <param name="key">Extension of the File</param>
        /// <returns>List of Files matching that Extension.</returns>
        public IReadOnlyList<FileInfo> this[string key] => Files.ContainsKey(key) ? Files[key] : null;

        #endregion Properties


        #region c'tor

        public ArgumentManager(IEnumerable<string> args)
        {
            var dict = new Dictionary<string, List<FileInfo>>();

            var options = new OptionSet {
                { "p|print", "print the set file(s).", p => ShouldPrint = p != null },
            };

            var files = options.Parse(args);

            foreach (var file in files)
            {
                try
                {
                    var f = new FileInfo(file);

                    if (dict.ContainsKey(f.Extension))
                        dict[f.Extension].Add(f);
                    else
                        dict.Add(f.Extension, new List<FileInfo> {f});
                }
                catch (Exception)
                {
                    //ignored
                }
            }

            Files = dict;
        }

        #endregion c'tor
    }
}
