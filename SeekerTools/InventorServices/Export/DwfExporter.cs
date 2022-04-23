using Inventor;

namespace SeekerTools.InventorServices.Export
{
    /// <summary>
    /// Export Inventor Documents to DWF
    /// </summary>
    internal class DwfExporter : IExportService
    {
        private readonly IExportManager _exportManager;
        /// <summary>
        /// Create a DWF exporter using a Export Manager (Translator Addin)
        /// </summary>
        /// <param name="exportManager"></param>
        /// <param name="exportFileName"></param>
        public DwfExporter(IExportManager exportManager)
        {
            _exportManager = exportManager;
        }
        /// <summary>
        /// Export Inventor AssemblyDocuments to DWF
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(AssemblyDocument asmDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(asmDoc, ".dwf");
            }
            else
            {
                fileName = exportFileName;
            }
            _exportManager.Export(asmDoc, fileName);
        }
        /// <summary>
        /// Export Inventor PartDocuments to DWF
        /// </summary>
        /// <param name="partDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(PartDocument partDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(partDoc, ".dwf");
            }
            else
            {
                fileName = exportFileName;
            }
            _exportManager.Export(partDoc, fileName);
        }
        /// <summary>
        /// Export Inventor DrawingDocuments to DWF
        /// </summary>
        /// <param name="drawDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(DrawingDocument drawDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(drawDoc, ".dwf");
            }
            else
            {
                fileName = exportFileName;
            }
            _exportManager.Export(drawDoc, fileName);
        }
    }
}
