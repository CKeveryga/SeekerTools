using Inventor;
using Microsoft.Extensions.Logging;
using System;

namespace SeekerTools.InventorServices.Export
{
    internal class StepExporter : IExportService
    {
        private readonly ILogger _logger;

        public StepExporter(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Export PartDocument to STEP file
        /// </summary>
        /// <param name="partDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(PartDocument partDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(partDoc, ".step");
            }
            else
            {
                fileName = exportFileName;
            }
            try
            {
                partDoc.SaveAs(fileName, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        /// <summary>
        /// Export AssemblyDocument to STEP file
        /// </summary>
        /// <param name="asmDoc"></param>
        /// <param name="exportFileName"></param>
        public void Export(AssemblyDocument asmDoc, string exportFileName = "")
        {
            string fileName;
            if (string.IsNullOrWhiteSpace(exportFileName))
            {
                fileName = FileNameHelper.FileName(asmDoc, ".step");
            }
            else
            {
                fileName = exportFileName;
            }
            try
            {
                asmDoc.SaveAs(fileName, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        /// <summary>
        /// Export PartDocument to STEP file [Not Implemented]
        /// </summary>
        /// <param name="drawDoc"></param>
        /// <param name="exportFileName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Export(DrawingDocument drawDoc, string exportFileName = "")
        {
            throw new NotImplementedException();
        }
    }
}
