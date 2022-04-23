using Inventor;
using SeekerTools.ExternalAbstractions;
using SeekerTools.InventorServices;

namespace SeekerTools.InventorAccess
{
    internal class OccurrenceExport : IOccurrenceTreater
    {
        public OccurrenceExport(IInventorContext inventorContext, IRevisionManager drawingManager)
        {
            _inventorContext = inventorContext;
            _drawingManager = drawingManager;
        }
        private readonly IRevisionManager _drawingManager;
        private readonly IInventorContext _inventorContext;

        public void AssemblyOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            var asmDoc = (AssemblyDocument)compOcc.Definition.Document;
            var invAsm = SeekerModels.MapAssemblyDocument(asmDoc);
            _inventorContext.AddAssembly(invAsm);
            var invDraw = _drawingManager.ManageExport(invAsm.PartNumber, asmDoc);
            _inventorContext.AddDrawing(invDraw);
        }

        public void PartOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            var partDoc = (PartDocument)compOcc.Definition.Document;
            var invPart = SeekerModels.MapPartDocument(partDoc);
            _inventorContext.AddPart(invPart);
            var invDraw = _drawingManager.ManageExport(invPart.PartNumber, partDoc);
            _inventorContext.AddDrawing(invDraw);
        }

        public void SheetMetalOcc(ComponentOccurrence compOcc, int depth, string parentId)
        {
            var partDoc = (PartDocument)compOcc.Definition.Document;
            var invPart = SeekerModels.MapPartDocument(partDoc);
            _inventorContext.AddPart(invPart);
            var invDraw = _drawingManager.ManageExport(invPart.PartNumber, partDoc);
            _inventorContext.AddDrawing(invDraw);
        }
    }
}

