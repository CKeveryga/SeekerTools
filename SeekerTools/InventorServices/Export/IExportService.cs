using Inventor;

namespace SeekerTools.InventorServices.Export
{
    public interface IExportService
    {
        /// <summary>
        /// Export an Inventor DrawingDocument
        /// </summary>
        /// <param name="drawDoc"></param>
        /// <param name="exportFullFileName"></param>
        void Export(DrawingDocument drawDoc, string exportFullFileName = "");
        /// <summary>
        /// Export an Inventor PartDocument
        /// </summary>
        /// <param name="partDoc"></param>
        /// <param name="exportFullFileName"></param>
        void Export(PartDocument partDoc, string exportFullFileName = "");
        /// <summary>
        /// Export an Inventor AssemblyDocument
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <param name="exportFullFileName"></param>
        void Export(AssemblyDocument asmDoc, string exportFullFileName = "");
    }
}