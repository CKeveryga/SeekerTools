using System;

namespace SeekerTools.DataModels
{
    public class DrawingModel : IDrawingModel
    {
        public string InventorId { get; set; }

        public int Revision { get; set; }

        public DateTime IdwDate { get; set; }

        public string PdfLocation { get; set; }

        public string DwgLocation { get; set; }

        public string StepLocation { get; set; }

        public string DwfLocation { get; set; }
    }
}
