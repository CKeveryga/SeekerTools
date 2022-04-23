using Inventor;
using System;
using System.Linq;
using SeekerTools.DataModels;
using SeekerTools.ExternalAbstractions;

namespace SeekerTools.InventorServices
{
    public class RevisionManager : IRevisionManager
    {
        private readonly DesignProjectManager _designProjectManager;
        private readonly Application _invApp;
        private readonly DrawingData _drawingData;
        private readonly IExporter _exporterService;
        private readonly string _projectPath;
        /// <summary>
        /// Export file manager
        /// </summary>
        /// <param name="invApp"></param>
        /// <param name="drawingData"></param>
        /// <param name="exporterService"></param>
        public RevisionManager(Application invApp, DrawingData drawingData, IExporter exporterService)
        {
            _invApp = invApp;
            _drawingData = drawingData;
            _designProjectManager = _invApp.DesignProjectManager;
            _exporterService = exporterService;
            string projectFileName = _designProjectManager.ActiveDesignProject.FullFileName;
            _projectPath = projectFileName.Substring(0, projectFileName.LastIndexOf("\\"));
        }
        /// <summary>
        /// Returns the drawing model with the exported file paths
        /// </summary>
        /// <param name="inventorId"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public IDrawingModel ManageExport(string inventorId, PartDocument document)
        {
            //does it have a drawing?
            string idw = _designProjectManager.ResolveFile(_projectPath, inventorId + ".idw");
            if (string.IsNullOrEmpty(idw))
                return null;
            // if not, exit, return error, idk?
            var drawingInfo = GetDrawingModel(inventorId);
            //check if dates match
            DateTime drawingModDate = Round(System.IO.File.GetLastWriteTime(idw), TimeSpan.FromSeconds(1));
            if (DatesAreEqual((DateTime)drawingInfo.IdwDate, drawingModDate))
                return null;
            // update IDW last modified date
            drawingInfo.IdwDate = drawingModDate;
            // open drawing for export
            DrawingDocument drawDoc = (DrawingDocument)_invApp.Documents.Open(idw, false);
            //export PDF, check if file exists
            _exporterService.PDF.Export(drawDoc, drawingInfo.PdfLocation);
            if (System.IO.File.Exists(drawingInfo.PdfLocation) == false)
                drawingInfo.PdfLocation = string.Empty;
            //export DXF, check if file exists
            _exporterService.DXF.Export(drawDoc, drawingInfo.DwgLocation);
            if (System.IO.File.Exists(drawingInfo.DwgLocation) == false)
                drawingInfo.DwgLocation = string.Empty;
            //export STEP, check if file exists
            _exporterService.STEP.Export(document, drawingInfo.StepLocation);
            if (System.IO.File.Exists(drawingInfo.StepLocation) == false)
                drawingInfo.StepLocation = string.Empty;
            //export DWF, check if file exists
            _exporterService.DWF.Export(document, drawingInfo.DwfLocation);
            if (System.IO.File.Exists(drawingInfo.DwfLocation) == false)
                drawingInfo.DwfLocation = string.Empty;
            // add drawing info to DB
            //_drawingData.InsertDrawingData(drawingInfo);

            drawDoc.Close();
            return drawingInfo;
        }
        /// <summary>
        /// Returns the drawing model with the exported file paths
        /// </summary>
        /// <param name="inventorId"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public IDrawingModel ManageExport(string inventorId, AssemblyDocument document)
        {
            //does it have a drawing?
            string idw = _designProjectManager.ResolveFile(_projectPath, inventorId + ".idw");
            if (string.IsNullOrEmpty(idw))
                return null;
            // if not, exit, return error, idk?
            var drawingInfo = GetDrawingModel(inventorId);
            //check if dates match
            DateTime drawingModDate = Round(System.IO.File.GetLastWriteTime(idw), TimeSpan.FromSeconds(1));
            if (DatesAreEqual((DateTime)drawingInfo.IdwDate, drawingModDate))
                return null;
            // update IDW last modified date
            drawingInfo.IdwDate = drawingModDate;
            // open drawing for export
            DrawingDocument drawDoc = (DrawingDocument)_invApp.Documents.Open(idw, false);
            drawingInfo.DwgLocation = string.Empty;
            drawingInfo.StepLocation = string.Empty;
            //export PDF, check if file exists
            _exporterService.PDF.Export(drawDoc, drawingInfo.PdfLocation);
            if (System.IO.File.Exists(drawingInfo.PdfLocation) == false)
                drawingInfo.PdfLocation = string.Empty;
            //export DWF, check if file exists
            _exporterService.DWF.Export(document, drawingInfo.DwfLocation);
            if (System.IO.File.Exists(drawingInfo.DwfLocation) == false)
                drawingInfo.DwfLocation = string.Empty;
            // add drawing info to DB
            //_drawingData.InsertDrawingData(drawingInfo);
            drawDoc.Close();
            return drawingInfo;
        }
        /// <summary>
        /// Rounds date to specified accuracy
        /// </summary>
        /// <param name="date"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        private DateTime Round(DateTime date, TimeSpan interval)
        {
            return new DateTime((long)Math.Round(date.Ticks / (double)interval.Ticks) * interval.Ticks);
        }
        /// <summary>
        /// Compare whether 2 dates are equal, down to the minute
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool DatesAreEqual(DateTime source, DateTime target)
        {
            if (DateTime.Compare(source.Date, target.Date) == 0)
            {
                if (source.Hour == target.Hour)
                {
                    if (source.Minute == target.Minute)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Gets the drawing model for the Inventor Id
        /// </summary>
        /// <param name="inventorId"></param>
        /// <returns></returns>
        private IDrawingModel GetDrawingModel(string inventorId)
        {
            IDrawingModel drawingModel = new DrawingModel();
            var revisions = _drawingData.GetDrawingData(inventorId);
            var currentDbData = revisions.FirstOrDefault();
            drawingModel.InventorId = inventorId;
            if (currentDbData == null)
            {
                drawingModel.Revision = 0;
                drawingModel.IdwDate = DateTime.MinValue;
            }
            else
            {
                drawingModel.Revision = currentDbData.Revision + 1;
                drawingModel.IdwDate = currentDbData.IdwDate;
            }
            drawingModel.PdfLocation = FileNameString(drawingModel, ".pdf");
            drawingModel.DwgLocation = FileNameString(drawingModel, ".dxf");
            drawingModel.StepLocation = FileNameString(drawingModel, ".step");
            drawingModel.DwfLocation = FileNameString(drawingModel, ".dwf");
            return drawingModel;
        }
        /// <summary>
        /// Creates file name from the drawing model
        /// </summary>
        /// <param name="drawingModel"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        private string FileNameString(IDrawingModel drawingModel, string fileExt)
        {
            return string.Format("{0}{1}_REV{2}{3}",
                _drawingData.ExportLocation, drawingModel.InventorId, drawingModel.Revision, fileExt);
        }
    }
}
