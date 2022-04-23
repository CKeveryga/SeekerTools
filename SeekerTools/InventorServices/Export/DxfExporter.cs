using Inventor;
using System;

namespace SeekerTools.InventorServices.Export
{
    internal class DxfExporter : IExportService
    {
        private readonly IExportManager _exportManager;
        /// <summary>
        /// Create a DXF exporter using a Export Manager (Translator Addin)
        /// </summary>
        /// <param name="exportManager"></param>
        public DxfExporter(IExportManager exportManager)
        {
            _exportManager = exportManager;
        }
        /// <summary>
        /// Export document to DXF
        /// </summary>
        /// <param name="drawDoc"></param>
        /// <param name="exportFullFileName"></param>
        public void Export(DrawingDocument drawDoc, string exportFullFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFullFileName))
            {
                fileName = FileNameHelper.FileName(drawDoc, ".dxf");
            }
            else
            {
                fileName = exportFullFileName;
            }
            _exportManager.Export(drawDoc, fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partDoc"></param>
        /// <param name="exportFullFileName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(PartDocument partDoc, string exportFullFileName = "")
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <param name="exportFullFileName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(AssemblyDocument asmDoc, string exportFullFileName = "")
        {
            throw new NotImplementedException();
        }
    }
}