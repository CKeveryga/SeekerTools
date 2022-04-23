using System;
using Inventor;
using SeekerTools;
using SeekerTools.ExternalAbstractions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using static SeekerTools.SeekerServices;

namespace SeekerServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();

            var invApp = SeekerServices.BackgroundInventor();

            var doc = invApp.Documents.Open(args[0]);

            if (doc.DocumentType != DocumentTypeEnum.kAssemblyDocumentObject)
                return;

            var aDoc = (AssemblyDocument)doc;

            var asm = SeekerModels.MapAssemblyDocument(aDoc);

            var exporter = SeekerServices.CreateExporter(invApp, logger, @"C:\Temp\Config.ini");

            var drawingData = SeekerServices.CreateDrawingData(new DrawingAccess(), @"C:\Temp\"); // args[1]

            var revManager = SeekerServices.CreateRevisionManager(invApp, drawingData, exporter);

            var invContext = SeekerServices.CreateInventorContext();

            var occTreater = SeekerServices.CreateOccurrenceTreater(invContext, revManager);

            var traverser = SeekerServices.CreateAssemblyTraverser(occTreater);

            traverser.OccLoop(aDoc.ComponentDefinition.Occurrences, 0, asm.PartNumber);

            var oneLineTraverser = CreateAssemblyTraverser(
                invApp, logger, new DrawingAccess(), @"C:\Temp\Drawings\");

            oneLineTraverser.OccLoop(aDoc.ComponentDefinition.Occurrences, 0, asm.PartNumber);

            foreach (var item in invContext.Assemblies)
            {
                Console.WriteLine(item.PartNumber);
            }
        }
    }

    internal class DrawingAccess : IDataAccess
    {
        public IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
