using DSACharacterSheet.Core.Printing.Exceptions;
using DSACharacterSheet.FileReader;
using PdfSharp;
using PdfSharp.Pdf;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DSACharacterSheet.Core.Printing
{
    public static class PrintingManager
    {
        #region Generate HTML

        public static string GenerateHtml(CharacterSheet sheet)
        {
            string template = "";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DSACharacterSheet.Core.Printing.PrintingTemplate.html"))
                using (var reader = new StreamReader(stream))
                {
                    template = reader.ReadToEnd();
                }

            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { CharacterSheet = sheet });

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
                    stream.Write(GenerateHtml(sheet));
            }
            catch (Exception e)
            {
                throw new PrintingException(path + "Can not be created.", e);
            }
        }

        #endregion Generate HTML

        #region Generate Pdf

        public static void GeneratePdf(CharacterSheet sheet, string path)
        {
            try
            {
                PdfDocument pdf = PdfGenerator.GeneratePdf(GenerateHtml(sheet), PageSize.A4);
                pdf.Save(path);
            }
            catch (Exception e)
            {
                throw new PrintingException(path + " can not be generated.", e);
            }
        }

        #endregion Generate Pdf
    }
}
