using Inventor;

namespace SeekerTools.InventorModels
{
    public interface IInvDoc
    {
        string Description { get; }
        string PartNumber { get; }
        string Vendor { get; }
        DocumentTypeEnum DocumentType { get; }
        PropertySet DesignTrackPropSet { get; }
        PropertySet CustomPropSet { get; }
        int Revision { get; }
        int StepNumber { get; }
        int CostCenter { get; }
    }
}