namespace Drachenhorn.Xml.Interfaces
{
    /// <summary>
    /// Interface for Savable objects.
    /// </summary>
    public interface ISavable
    {
        /// <summary>
        /// Saving-Method
        /// </summary>
        /// <param name="path">Path to save to.</param>
        void Save(string path);
    }
}
