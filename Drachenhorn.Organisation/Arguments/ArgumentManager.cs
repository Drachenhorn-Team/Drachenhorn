using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mono.Options;

namespace Drachenhorn.Organisation.Arguments
{
    public class ArgumentManager
    {
        #region Properties

        public bool ShouldPrint { get; private set; } = false;

        private readonly List<FileInfo> _files = new List<FileInfo>();

        public IReadOnlyList<FileInfo> Files => _files;

        #endregion Properties


        #region c'tor

        public ArgumentManager(IEnumerable<string> args, IEnumerable<string> extensions)
        {
            var options = new OptionSet {
                { "p|print", "print the set file(s).", p => ShouldPrint = p != null },
            };

            var files = options.Parse(args);

            foreach (var file in files)
            {
                try
                {
                    var f = new FileInfo(file);
                    
                    if (extensions.Contains(f.Extension))
                        _files.Add(f);
                }
                catch (Exception)
                {
                    //ignored
                }
            }
        }

        #endregion c'tor
    }
}
