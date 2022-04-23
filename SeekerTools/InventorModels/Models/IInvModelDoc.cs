using Inventor;

namespace SeekerTools.InventorModels
{
    public interface IInvModelDoc : IInvDoc
    {
        double Weight { get; }
        BOMQuantityTypeEnum BomQtyType { get; }
        BOMStructureEnum BomStructure { get; }
    }
}
