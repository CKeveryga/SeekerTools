using SeekerTools.InventorServices.Export;

namespace SeekerTools.InventorServices
{
    public interface IExporter
    {
        IExportService DWF { get; }
        IExportService DWG { get; }
        IExportService DXF { get; }
        IExportService PDF { get; }
        IExportService STEP { get; }
    }
}