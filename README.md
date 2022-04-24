# Seeker Tools
## About
Inventor tools class library
### Reference Libraries
```ruby
using System;
using Inventor;
using SeekerTools;
using SeekerTools.ExternalAbstractions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using static SeekerTools.SeekerServices;
```
### Start Inventor in the Background with Optimal Performance Settings
```ruby
var invApp = SeekerServices.BackgroundInventor();
```
### Open Assembly Document and Map
```ruby
var doc = invApp.Documents.Open(@"C:\Temp\AssemblyOne.iam");

if (doc.DocumentType != DocumentTypeEnum.kAssemblyDocumentObject)
return;

var aDoc = (AssemblyDocument)doc;

var asm = SeekerModels.MapAssemblyDocument(aDoc);

Console.WriteLine("PN: {0}\nDescription: {1}\nVendor: {2}", asm.PartNumber, asm.Description, asm.Vendor);
```
### Open Part and Get Drawing
```ruby
var partDoc = (PartDocument)invApp.Documents.Open(@"C:\Temp\PartOne.ipt");

var mappedPart = SeekerModels.MapPartDocument(partDoc);

string projectFileName = invApp.DesignProjectManager.ActiveDesignProject.FullFileName;

string projectPath = projectFileName.Substring(0, projectFileName.LastIndexOf("\\"));

string idw = invApp.DesignProjectManager.ResolveFile(projectPath, mappedPart.PartNumber + ".idw");

var drawDoc = (DrawingDocument)invApp.Documents.Open(idw);
```
### Create Exporter and Export to Original Location
```ruby
var exporter = SeekerServices.CreateExporter(invApp, logger, @"C:\Temp\Config.ini");

exporter.DXF.Export(drawDoc);
exporter.PDF.Export(drawDoc);
exporter.DWG.Export(drawDoc);

exporter.DWF.Export(partDoc);
exporter.STEP.Export(partDoc);
```
### Create Classes to Work With Seeker
Implement IOccurrenceTreater for custom usage within assembly loop
```ruby
    internal class CustomTreat : IOccurrenceTreater
    {
        public void AssemblyOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            // Custom work with compOcc
        }

        public void PartOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            // Custom work with compOcc
        }

        public void SheetMetalOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            // Custom work with compOcc
        }
    }
```
### Create Data Access Class to Retrieve Drawing Info
Implement IDataAccess
Write method to connect to data base with drawing info.
```ruby
    internal class DrawingAccess : IDataAccess
    {
        public IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            throw new System.NotImplementedException();
        }
    }
```
