using DSACharacterSheet.Core.Printing.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Xml.Sheet;
using RazorLight;

namespace DSACharacterSheet.Core.Printing
{
    public static class PrintingManager
    {
        #region Generate HTML

        public static string GenerateHtml(CharacterSheet sheet)
        {
            string template = "";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DSACharacterSheet.Core.Printing.PrintingTemplate.cshtml"))
                using (var reader = new StreamReader(stream))
                {
                    template = reader.ReadToEnd();
                }
            
            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();

            return Task.Run<string>(() => engine.CompileRenderAsync("templateKey", template, sheet)).Result;
        }

        public static void GenerateHtml(CharacterSheet sheet, string path)
        {
            var file = new FileInfo(path);

            if (!file.Directory.Exists)
                throw new DirectoryNotFoundException("Directory " + file.Directory.FullName + " does not exist.");

            try
            {
                if (file.Exists)
                    file.Delete();

                using (var stream = file.CreateText())
                    stream.Write(GenerateHtml(sheet));
            }
            catch (Exception e)
            {
                throw new PrintingException(path + "Can not be created.", e);
            }
        }

        #endregion Generate HTML
    }
}
