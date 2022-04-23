using Inventor;

namespace SeekerTools.InventorModels
{
    public interface IInvAsmDoc : IInvDoc, IInvModelDoc
    {
        AssemblyComponentDefinition AsmCompDef { get; }
    }
}