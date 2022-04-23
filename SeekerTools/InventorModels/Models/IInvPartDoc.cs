using Inventor;

namespace SeekerTools.InventorModels
{
    public interface IInvPartDoc : IInvModelDoc, IInvDoc
    {
        double Length { get; }
        double Width { get; }
        double Thickness { get; }
        string SubType { get; }
        string Features { get; }
        string Material { get; }
        string StockType { get; }
        PartComponentDefinition PartCompDef { get; }
    }
}