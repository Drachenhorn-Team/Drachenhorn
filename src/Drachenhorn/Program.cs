using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Drachenhorn.Log;
using Mono.Options;

namespace Drachenhorn
{
    class Program
    {
        private static VerbosityLevel _verbosity;
        private static bool _showHelp;




        static void Main(string[] args)
        {
            var options = SetupOptions();

            var extras = options.Parse(args);

            if (_showHelp)
            {
                ShowHelp(options);
                return;
            }

            Console.WriteLine("Hello World!");
        }

        #region Commandline-Options

        private static OptionSet SetupOptions()
        {
            // these variables will be set when the command line is parsed
            
            _verbosity = VerbosityLevel.Fatal;
            // thses are the available options, not that they set the variables

            var options = new OptionSet {
                { "v|verbosity=", "set verbosity to None(0), Fatal(1), Error(2), Warning(3), Info(4), Debug(5)", value =>
                    {
                        _verbosity = Enum.TryParse(value, out _verbosity) ? _verbosity : VerbosityLevel.Debug;
                    }
                },
                { "h|help", "show this message and exit", h => _showHelp = h != null },
            };

            return options;
        }

        private static void ShowHelp(OptionSet options)
        {
            //TODO: Proper messages

            Console.WriteLine("Usage: OptionsSample.exe [OPTIONS]+ message");
            Console.WriteLine("Generates Character-Sheets based on the 'the dark eye' rule system.");
            Console.WriteLine();

            // output the options
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }

        #endregion Commandline-Options
    }
}
