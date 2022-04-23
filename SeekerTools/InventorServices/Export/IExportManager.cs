using Inventor;

namespace SeekerTools.InventorServices.Export
{
    /// <summary>
    /// Manage exporters for Inventor Documents
    /// </summary>
    public interface IExportManager
    {
        /// <summary>
        /// Manage method to export an Inventor DrawingDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        void Export(DrawingDocument document, string exportFullFileName);
        /// <summary>
        /// Manage method to export an Inventor PartDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        void Export(PartDocument document, string exportFullFileName);
        /// <summary>
        /// Manage method to export an Inventor AssemblyDocument
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exportFullFileName"></param>
        void Export(AssemblyDocument document, string exportFullFileName);
    }
}