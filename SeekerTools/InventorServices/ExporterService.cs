using Microsoft.Extensions.Logging;
using SeekerTools.InventorServices.Export;

namespace SeekerTools.InventorServices
{
    /// <summary>
    /// Simple export service for Inventor
    /// </summary>
    public class ExporterService : IExporter
    {
        private readonly ILogger _logger;
        private readonly IExportService _pdfExporter;
        private readonly IExportService _dwgExporter;
        private readonly IExportService _dwfExporter;
        private readonly IExportService _dxfExporter;
        private readonly IExportService _stepExporter;
        /// <summary>
        /// Create and configure Inventor Exporters
        /// </summary>
        /// <param name="invApp"></param>
        public ExporterService(Inventor.Application invApp, ILogger logger, string dwgDwfIniFile = "")
        {
            var transientObjects = invApp.TransientObjects;
            var addins = invApp.ApplicationAddIns;

            _pdfExporter = new PdfExporter(new AddinManager(transientObjects, addins, AddinManager.ExporterAddinEnum.PDF, _logger));

            _dwgExporter = new DwgExporter(new AddinManager(transientObjects, addins, AddinManager.ExporterAddinEnum.DWG, _logger, dwgDwfIniFile));

            _dwfExporter = new DwfExporter(new AddinManager(transientObjects, addins, AddinManager.ExporterAddinEnum.DWF, _logger, dwgDwfIniFile));

            _dxfExporter = new DxfExporter(new AddinManager(transientObjects, addins, AddinManager.ExporterAddinEnum.DXF, _logger));

            _stepExporter = new StepExporter(_logger);
            _logger = logger;
        }
        /// <summary>
        /// PDF Exporter
        /// </summary>
        public IExportService PDF => _pdfExporter;
        /// <summary>
        /// DWG Exporter
        /// </summary>
        public IExportService DWG => _dwgExporter;
        /// <summary>
        /// DWF Exporter
        /// </summary>
        public IExportService DWF => _dwfExporter;
        /// <summary>
        /// STEP File Exporter
        /// </summary>
        public IExportService STEP => _stepExporter;
        /// <summary>
        /// DXF File Exporter
        /// </summary>
        public IExportService DXF => _dxfExporter;
    }
}
