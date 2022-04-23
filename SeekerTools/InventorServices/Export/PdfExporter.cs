using Inventor;
using System;

namespace SeekerTools.InventorServices.Export
{
    /// <summary>
    /// Export Inventor Documents to PDF
    /// </summary>
    internal class PdfExporter : IExportService
    {
        private readonly IExportManager _exportManager;
        /// <summary>
        /// Create a PDF exporter using a Export Manager (Translator Addin)
        /// </summary>
        /// <param name="exportManager"></param>
        /// <param name="exportFileName"></param>
        public PdfExporter(IExportManager exportManager)
        {
            _exportManager = exportManager;
        }
        /// <summary>
        /// Export Inventor Drawing to PDF
        /// </summary>
        /// <param name="drawDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(DrawingDocument drawDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(drawDoc, ".pdf");
            }
            else
            {
                fileName = exportFileName;
            }
            _exportManager.Export(drawDoc, fileName);
        }
        /// <summary>
        /// Export Inventor Part to PDF [Not Implemented]
        /// </summary>
        /// <param name="partDoc"></param>
        /// <param name="exportFileName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(PartDocument partDoc, string exportFileName = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Export Inventor Assembly to PDF [Not Implemented]
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <param name="exportFileName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(AssemblyDocument asmDoc, string exportFileName = "")
        {
            throw new NotImplementedException();
        }
    }
}
