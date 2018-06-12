using System;
using System.IO;
using System.Reflection;
using System.Text;
using Drachenhorn.Core.Printing.Exceptions;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Xml.Sheet;
using RazorLight;

namespace Drachenhorn.Core.Printing
{
    public static class PrintingManager
    {
        #region Generate HTML

        public static string GenerateHtml(CharacterSheet sheet)
        {
            string template = "";

            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("Drachenhorn.Core.Printing.Templates.CharacterSheet.cshtml"))
            using (StreamReader reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }
            
            var engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();

            string result = engine.CompileRenderAsync("templateKey", template, new CharacterSheetViewModel(sheet)).Result;

            return result;
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
                {
                    stream.Write(GenerateHtml(sheet));
                }
            }
            catch (Exception e)
            {
                throw new PrintingException(path + "Can not be created.", e);
            }
        }

        #endregion Generate HTML
    }
}