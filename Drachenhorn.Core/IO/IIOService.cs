using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet;

namespace Drachenhorn.Core.IO
{
    public interface IIoService
    {
        void SaveDataDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            byte[] data,
            bool openAfterFinished = false);

        byte[] OpenDataDialog(
            string fileExtension,
            string fileTypeName,
            string title);

        void SaveStringDialog(
            string fileName,
            string fileExtension,
            string fileTypeName,
            string title,
            string text,
            bool openAfterFinished = false);

        string OpenStringDialog(
            string fileExtension,
            string fileTypeName,
            string title);

        void SaveAs(ISavable savable, string fileName, string extension, string fileTypeName, string title);

        CharacterSheet OpenCharacterSheet();

        void SaveString(string path, string text, bool writeNew = true);
    }
}