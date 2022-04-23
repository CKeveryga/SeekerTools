using SeekerTools.InventorServices;
using Microsoft.Extensions.Logging;
using SeekerTools.ExternalAbstractions;
using SeekerTools.InventorAccess;
using Inventor;

namespace SeekerTools
{
    public static class SeekerServices
    {
        /// <summary>
        /// Starts Inventor with performance background options
        /// </summary>
        /// <returns></returns>
        public static Application BackgroundInventor()
            => new InventorStarter().StartInventor();
        /// <summary>
        /// Creates a Revision Manager
        /// Manages all exports in a centralized location
        /// </summary>
        /// <param name="invApp"></param>
        /// <param name="drawingData"></param>
        /// <param name="exporterService"></param>
        /// <returns></returns>
        public static IRevisionManager CreateRevisionManager(
            Application invApp, DrawingData drawingData, IExporter exporterService)
            => new RevisionManager(invApp, drawingData, exporterService);
        /// <summary>
        /// Gives access to drawing data from DB
        /// </summary>
        /// <param name="dataAccess"></param>
        /// <param name="drawingLocation"></param>
        /// <returns></returns>
        public static DrawingData CreateDrawingData(IDataAccess dataAccess, string drawingLocation) 
            => new DrawingData(dataAccess, drawingLocation);
        /// <summary>
        /// Creates an Exporter for PDF, DWG, DWF, DXF, and STEP files
        /// </summary>
        /// <param name="invApp"></param>
        /// <param name="logger"></param>
        /// <param name="dwgDwfIniFile"></param>
        /// <returns></returns>
        public static IExporter CreateExporter(
            Inventor.Application invApp, ILogger logger, string dwgDwfIniFile = "") 
            => new ExporterService(invApp, logger, dwgDwfIniFile);
        /// <summary>
        /// Inventor Context to store part, assembly and drawing info
        /// </summary>
        /// <returns></returns>
        public static IInventorContext CreateInventorContext() 
            => new InventorContext();
        /// <summary>
        /// Creates Occurrence treater to get data and export drawings
        /// </summary>
        /// <param name="inventorContext"></param>
        /// <param name="revManager"></param>
        /// <returns></returns>
        public static IOccurrenceTreater CreateOccurrenceTreater(
            IInventorContext inventorContext, IRevisionManager revManager) 
            => new OccurrenceExport(inventorContext, revManager);
        /// <summary>
        /// Creates means of traversing through all levels of an assembly
        /// </summary>
        /// <param name="occurrenceTreater"></param>
        /// <returns></returns>
        public static IAssemblyTraverser CreateAssemblyTraverser(
            IOccurrenceTreater occurrenceTreater) 
            => new AssemblyTraverser(occurrenceTreater);
        /// <summary>
        /// Creates an AssemblyTraverser using all Seeker Services
        /// </summary>
        /// <param name="invApp"></param>
        /// <param name="logger"></param>
        /// <param name="dataAccess"></param>
        /// <param name="drawingsLocation"></param>
        /// <returns></returns>
        public static IAssemblyTraverser CreateAssemblyTraverser(Application invApp, ILogger logger, IDataAccess dataAccess, string drawingsLocation)
        {
            return CreateAssemblyTraverser(
                CreateOccurrenceTreater(
                    CreateInventorContext(),
                    CreateRevisionManager(invApp,
                    CreateDrawingData(dataAccess, drawingsLocation),
                    CreateExporter(invApp, logger))));
        }
    }
}