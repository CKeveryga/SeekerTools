using Inventor;
using System;

namespace SeekerTools.InventorServices.Export
{
    /// <summary>
    /// Export Inventor Documents to DWG
    /// </summary>
    internal class DwgExporter : IExportService
    {
        private readonly IExportManager _exportManager;
        /// <summary>
        /// Create a DWG exporter using a Export Manager (Translator Addin)
        /// </summary>
        /// <param name="exportManager"></param>
        public DwgExporter(IExportManager exportManager)
        {
            _exportManager = exportManager;
        }
        /// <summary>
        /// Export Inventor Documents to DWG
        /// </summary>
        /// <param name="drawDoc"></param>
        public void Export(DrawingDocument drawDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(drawDoc, ".dwg");
            }
            else
            {
                fileName = exportFileName;
            }
            _exportManager.Export(drawDoc, fileName);
        }
        /// <summary>
        /// Export Inventor Documents to DWG
        /// </summary>
        /// <param name="partDoc"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(PartDocument partDoc, string exportFileName = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Export Inventor Documents to DWG
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(AssemblyDocument asmDoc, string exportFileName = "")
        {
            throw new NotImplementedException();
        }
    }
}
