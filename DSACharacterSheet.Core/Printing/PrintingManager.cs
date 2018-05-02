using DSACharacterSheet.Core.Printing.Exceptions;
using DSACharacterSheet.Xml.Sheet;
using System;
using System.IO;

namespace DSACharacterSheet.Core.Printing
{
    public static class PrintingManager
    {
        #region Generate HTML

        public static string GenerateHtml(CharacterSheet sheet)
        {
            //string template = "";

            //template = Encoding.Default.GetString(PrintingTemplates.CharacterSheet);

            //var engine = new RazorLightEngineBuilder()
            //    .UseMemoryCachingProvider()
            //    .Build();

            //string result = engine.CompileRenderAsync("templateKey", template, new CharacterSheetViewModel(sheet)).Result;

            //return result;

            return "";
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