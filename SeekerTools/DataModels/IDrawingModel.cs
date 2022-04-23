using System;

namespace SeekerTools.DataModels
{
    public interface IDrawingModel
    {

        string InventorId { get; set; }

        DateTime IdwDate { get; set; }

        int Revision { get; set; }

        string PdfLocation { get; set; }

        string DwgLocation { get; set; }

        string StepLocation { get; set; }

        string DwfLocation { get; set; }
    }
}