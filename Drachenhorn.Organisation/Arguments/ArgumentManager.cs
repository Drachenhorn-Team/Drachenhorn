using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Options;

namespace Drachenhorn.Organisation.Arguments
{
    public class ArgumentManager
    {
        #region c'tor

        public ArgumentManager(IEnumerable<string> args)
        {
            var dict = new Dictionary<string, List<FileInfo>>();

            var options = new OptionSet
            {
                {"p|print", "print the set file(s).", p => ShouldPrint = p != null}
            };

            var files = options.Parse(args.Skip(1));

            foreach (var file in files)
            {
                if (file.ToLower().StartsWith("drachenhorn:"))
                {
                    var text = file.Substring(12, file.Length - 12);

                    UrlScheme = new Uri(text);

                    continue;
                }

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

        #region Properties

        public bool ShouldPrint { get; private set; }

        private Dictionary<string, List<FileInfo>> Files { get; }

        /// <summary>
        ///     Return the file with the given File Extension
        /// </summary>
        /// <param name="key">Extension of the File</param>
        /// <returns>List of Files matching that Extension.</returns>
        public IReadOnlyList<FileInfo> this[string key] => Files.ContainsKey(key) ? Files[key] : null;

        public Uri UrlScheme { get; }

        #endregion Properties
    }
}