using Inventor;
using SeekerTools.DataModels;

namespace SeekerTools.InventorServices
{
    public interface IRevisionManager
    {
        IDrawingModel ManageExport(string inventorId, AssemblyDocument document);
        IDrawingModel ManageExport(string inventorId, PartDocument document);
    }
}