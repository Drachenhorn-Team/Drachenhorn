using DSACharacterSheet.Xml.Sheet;
using DSACharacterSheet.Xml.Template;

namespace DSACharacterSheet.Core.IO
{
    public interface IIOService
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

        void SaveAsCharacterSheet(CharacterSheet sheet);

        CharacterSheet OpenCharacterSheet();

        void SaveString(string path, string text);
    }
}